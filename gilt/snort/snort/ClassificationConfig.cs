using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using gilt.dblinq;
using gilt.util;

namespace gilt.snort
{
    /// <summary>
    /// Classification.config
    /// 
    /// Line Format:
    /// 
    /// Config classification:shortname,short description,priority
    /// </summary>
    class ClassificationConfig : GenericConfigReaderWriter
    {
        public ClassificationConfig(string FileName)
            : base(FileName, ",", new string[] { "config classification:" })
        {
        }

        public override void WriteToDatabase()
        {
            this.InsertDefaultSignatureClassification();

            int lineCount = _parsedLines.Count;
            List<SignatureClassification> sigs = new List<SignatureClassification>();
            using (TransactionScope trans = new TransactionScope())
            {
                using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                {
                    for (int i = 0; i < lineCount; i++)
                    {
                        string[] vals = _parsedLines[i];
                        try
                        {
                            SignatureClassification sc = new SignatureClassification();
                            sc.ClassificationId = i + 1;
                            sc.Name = vals[0].ToString();
                            sc.Description = vals[1].ToString();
                            sc.DefaultPriority = Convert.ToByte(vals[2]);
                            sigs.Add(sc);
                        }
                        catch (Exception exc)
                        {
                            _giltSetupLogger.Error(exc);
                        }
                    }
                    context.SignatureClassifications.InsertAllOnSubmit<SignatureClassification>(sigs);
                    context.SubmitChanges();
                }
                trans.Complete();
            }
        }

        private void InsertDefaultSignatureClassification()
        {
            _giltSetupLogger.Info(String.Format("Attempting SignatureClassification Insert"));
            using (TransactionScope trans = new TransactionScope())
            {
                using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                {
                    SignatureClassification prevSigClass = (from s in context.SignatureClassifications
                                                            where
                                                                (s.ClassificationId == 0)
                                                            select s).ToList().FirstOrDefault();
                    if (null == prevSigClass)
                    {
                        _giltSetupLogger.Info(String.Format("Inserting Sensor"));
                        //_context.ExecuteCommand("SET IDENTITY_INSERT [SignatureClassification] ON");
                        context.ExecuteCommand("INSERT INTO [SignatureClassification] ([ClassificationId],[Name],[Description]) VALUES (0,'unknown','unknown')");
                        //_context.ExecuteCommand("SET IDENTITY_INSERT [SignatureClassification] OFF");                        
                    }
                    trans.Complete();
                }
                _giltSetupLogger.Info(String.Format("Sensor Created"));
            }
        }
    }
}
