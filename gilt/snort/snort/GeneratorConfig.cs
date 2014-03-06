using gilt.dblinq;
using gilt.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace gilt.snort
{
    /// <summary>
    /// gen-msg.map
    /// 
    /// Line Format:
    /// 
    /// generatorid || alertid || MSG
    /// </summary>
    public class GeneratorConfig : GenericConfigReaderWriter
    {
        /// <summary>
        /// Create snort generator config
        /// </summary>
        /// <param name="FileName">The path to gen-msg.map file</param>
        public GeneratorConfig(string FileName)
            : base(FileName)
        {
        }

        /// <summary>
        /// Write the GID signature to the database
        /// </summary>
        public override void WriteToDatabase()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            this.InsertDefaultSignature();

            int lineCount = _parsedLines.Count;
            List<Signature> sigs = new List<Signature>();
            using (TransactionScope trans = new TransactionScope())
            {
                using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                {
                    for (int i = 0; i < lineCount; i++)
                    {
                        string[] vals = _parsedLines[i];
                        try
                        {
                            Signature s = new Signature();
                            s.ClassificationId = 0;
                            s.Priority = 0;
                            s.Revision = 0;

                            s.GeneratorId = Convert.ToDecimal(vals[0]);
                            s.SignatureIdInternal = Convert.ToDecimal(vals[1]);
                            s.Name = vals[2].ToString();
                            sigs.Add(s);
                        }
                        catch (Exception exc)
                        {
                            _giltSetupLogger.Error(exc);
                        }
                    }
                    context.Signatures.InsertAllOnSubmit<Signature>(sigs);
                    context.SubmitChanges();
                }
                trans.Complete();
            }
            _giltSetupLogger.Info(String.Format("Signature Table: {0} rows, {1} ms", sigs.Count, sw.ElapsedMilliseconds));
        }

        /// <summary>
        /// Insert a signature with id = 0
        /// </summary>
        private void InsertDefaultSignature()
        {
            using (TransactionScope trans = new TransactionScope())
            {
                using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                {
                    Signature prevSig = (from s in context.Signatures
                                         where
                                             (s.Id == 0)
                                         select s).ToList().FirstOrDefault();
                    if (null == prevSig)
                    {
                        context.ExecuteCommand("SET IDENTITY_INSERT [Signature] ON");
                        context.ExecuteCommand("INSERT INTO [Signature] ([Id],[Name],[ClassificationId]) VALUES (0,'unknown',0)");
                        context.ExecuteCommand("SET IDENTITY_INSERT [Signature] OFF");
                    }
                    context.SubmitChanges();
                }
                trans.Complete();
            }
        }
    }
}
