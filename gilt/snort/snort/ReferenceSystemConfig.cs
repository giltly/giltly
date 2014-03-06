using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Transactions;
using gilt.dblinq;
using gilt.util;

namespace gilt.snort
{
    /// <summary>
    /// Reference.config
    /// 
    /// Line Format
    /// 
    /// config reference: system URL
    /// </summary>
    class ReferenceSystemConfig : GenericConfigReaderWriter
    {
        public ReferenceSystemConfig(string FileName)
            : base(FileName, " ", new string[] {" ", "config reference:"})
        { 
        }

        public override void WriteToDatabase()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int lineCount = _parsedLines.Count;
            List<ReferenceSystem> refs = new List<ReferenceSystem>();
            using (TransactionScope trans = new TransactionScope())
            {
                using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                {
                    for (int i = 0; i < lineCount; i++)
                    {
                        string[] vals = _parsedLines[i];
                        try
                        {
                            ReferenceSystem refss = new ReferenceSystem();
                            refss.Name = vals[0].ToString();
                            refss.URL = vals[1].ToString();
                            refs.Add(refss);
                        }
                        catch (Exception exc)
                        {
                            _giltSetupLogger.Error(exc);
                        }
                    }
                    context.ReferenceSystems.InsertAllOnSubmit<ReferenceSystem>(refs);
                    context.SubmitChanges();
                }
                trans.Complete();
            }
            _giltSetupLogger.Info(String.Format("ReferenceSystem Table: {0} rows, {1} ms", refs.Count, sw.ElapsedMilliseconds));
        }
    }
}
