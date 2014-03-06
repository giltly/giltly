using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using gilt.dblinq;
using gilt.util;

namespace gilt.snort
{
    /// <summary>
    /// SID-MSG.map
    /// 
    /// SID is Snort Id
    /// GID is Snort Generator Id
    /// 
    /// Line Format:
    /// 
    /// Version 1
    /// SID || MSG || REF 1 || REF N
    /// 
    /// Version 2 -- not currently being used
    /// GID || SID || REV || CLASSIFICATION || PRIORITY || MSG || REF 1 || REF N
    /// </summary>
    public sealed class SnortMsgMapConfig : GenericConfigReaderWriter
    {
        /// <summary>
        /// Snort Id to Signature name dictionary
        /// </summary>
        public static Dictionary<uint, string> SnortIdToSignatureName
        {
            get { return _snortIdToSignatureNameLookup; }
        }
        private static Dictionary<uint, string> _snortIdToSignatureNameLookup = null;
        private static ILookup<string,ReferenceSystem> _refSystems = null;

        private const int INDEX_REFERENCE_SYS_NAME = 0;
        private const int INDEX_REFERENCE_SYS_VALUE = 1;

        /// <summary>
        /// Crete a snort-msg.map object
        /// </summary>
        /// <param name="FileName">The full path to SID-MSG.map file</param>
        public SnortMsgMapConfig(string FileName)
            : base(FileName)
        {
            _snortIdToSignatureNameLookup = new Dictionary<uint, string>();
        }

        /// <summary>
        /// Write the Generator information to the database
        /// </summary>
        public override void WriteToDatabase()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int lineCount = _parsedLines.Count;
            List<Reference> refs = new List<Reference>();            

            using (TransactionScope trans = new TransactionScope())
            {
                using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                {
                    //use lowercase system name
                    _refSystems = (from r in context.ReferenceSystems select r).ToLookup(rs => rs.Name.ToLower(), rs => rs);

                    for (int i = 0; i < lineCount; i++)
                    {
                        string[] vals = _parsedLines[i];
                        try
                        {
                            _snortIdToSignatureNameLookup.Add(Convert.ToUInt32(vals[0]), vals[1]);
                            //
                            // the section below parses items that look like the following
                            //
                            // || bugtraq,16202 || cve,2005-2340
                            int refSystemCount = vals.Length;
                            for (int j = 2; j < refSystemCount; j++)
                            {
                                string trimmed = vals[j];
                                foreach (string t in _lineTrims)
                                {
                                    trimmed = Regex.Replace(trimmed, "^" + t, string.Empty);
                                }
                                string[] rval = trimmed.Split(",".ToCharArray());

                                Reference r = new Reference();
                                //use lowercase system name
                                ReferenceSystem rs = _refSystems[rval[INDEX_REFERENCE_SYS_NAME].ToLower()].FirstOrDefault();
                                if (null == rs)
                                {
                                    //the line contains only a SID and MSG
                                    // there is not enough information to store a new reference
                                    _giltSetupLogger.Debug(String.Format("Skipped Line: {0}", trimmed));
                                }
                                else
                                {
                                    r.SystemId = rs.Id;
                                    r.Tag = String.Format("{0}{1}", rs.URL, rval[INDEX_REFERENCE_SYS_VALUE]);
                                    if (null != r.Tag)
                                    {
                                        refs.Add(r);
                                    }
                                }                                
                            }
                        }
                        catch (Exception exc)
                        {
                            _giltSetupLogger.Error(exc);
                        }
                    }
                    context.References.InsertAllOnSubmit<Reference>(refs);
                    context.SubmitChanges();
                }
                trans.Complete();
            }           
            sw.Stop();
            _giltSetupLogger.Info(String.Format("Reference Table: {0} rows, {1} ms", refs.Count, sw.ElapsedMilliseconds));
        }
    }
}
