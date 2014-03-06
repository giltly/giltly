using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using NLog;
using gilt.dblinq;
using gilt.util;

namespace gilt.snort
{
    /// <summary>
    /// Processes the following snort files:
    /// classification.config
    /// reference.config
    /// sid-msg.map
    /// gen-msp.map
    /// 
    /// Inserts all of the relevant information into the database
    /// </summary>
    public class SnortConfigReaderWriter : IGenericConfigWriter, IGenericConfigReader
    {
        private string _connectingString = null;
        private string _classificationConfigName = null;
        private string _referenceConfigName = null;
        private string _snortMsgMapConfigName = null;
        private string _generatorMapConfigName = null;
        private static Logger _giltSetupLogger = LogManager.GetLogger("giltsetup");

        private ClassificationConfig _classificationConfig = null;
        private ReferenceSystemConfig _referenceConfig = null;
        private SnortMsgMapConfig _snortMsgMapConfig = null;
        private GeneratorConfig _generatorMapConfig = null;

        /// <summary>
        /// Create a snort configuration file reader
        /// </summary>
        /// <param name="ConnectionString">The database connnection string</param>
        /// <param name="ClassificationConfig">The full path to classification.config</param>
        /// <param name="ReferenceConfig">The full path to reference.config</param>
        /// <param name="SensorMsgMapConfig">The full path to sid-msg.map</param>
        /// <param name="GeneratorMapConfig">The full path to gen-msg.map</param>
        public SnortConfigReaderWriter(   
            string ConnectionString,
            string ClassificationConfig,
            string ReferenceConfig,
            string SensorMsgMapConfig,
            string GeneratorMapConfig)
        {
            _connectingString = ConnectionString;
            _classificationConfigName = ClassificationConfig;
            _referenceConfigName = ReferenceConfig;
            _snortMsgMapConfigName = SensorMsgMapConfig;
            _generatorMapConfigName = GeneratorMapConfig;

            _classificationConfig = new ClassificationConfig(_classificationConfigName);
            _referenceConfig = new ReferenceSystemConfig(_referenceConfigName);
            _generatorMapConfig = new GeneratorConfig(_generatorMapConfigName);
            _snortMsgMapConfig = new SnortMsgMapConfig(_snortMsgMapConfigName);

            _giltSetupLogger.Info(String.Format("Connection String: {0}", _connectingString ));
            _giltSetupLogger.Info(String.Format("App Settings Connection String: {0}", AppSettings.DatabaseConnectionString));
            
            _giltSetupLogger.Info(String.Format("classification.config: {0}", _classificationConfigName));
            _giltSetupLogger.Info(String.Format("reference.config: {0}", _referenceConfigName));
            _giltSetupLogger.Info(String.Format("sid-msg-map: {0}", _snortMsgMapConfigName));
            _giltSetupLogger.Info(String.Format("gen-msg.map: {0}", _generatorMapConfigName));
        }

        /// <summary>
        /// Read all Snort Configuration Files from Disk
        /// </summary>
        public void ReadFile()
        {
            _classificationConfig.ReadFile();
            _referenceConfig.ReadFile();
            _generatorMapConfig.ReadFile();
            _snortMsgMapConfig.ReadFile();
        }

        /// <summary>
        /// Write the snort configuration entries to the database
        /// </summary>
        public void WriteToDatabase()
        {            
            this.ReadFile();
            this.InsertSensor();
        }

        /// <summary>
        /// Insert the sensor configuration 
        /// </summary>
        public void InsertSensor()
        {
            _giltSetupLogger.Info(String.Format("Attempting Sensor Insert"));
            using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
            {
                Sensor prevSensor = (from s in context.Sensors
                                     where
                                         (s.Id == 0)
                                     select s).ToList().FirstOrDefault();
                if (null == prevSensor)
                {
                    _giltSetupLogger.Info(String.Format("Inserting Sensor"));
                    using (TransactionScope trans = new TransactionScope())
                    {
                        context.ExecuteCommand("SET IDENTITY_INSERT [Sensor] ON");
                        //TODO:  Sensor needs to come from app.config
                        context.ExecuteCommand("insert into sensor ([id], [Hostname], [Interface], [Filter], [DetailId], [EncodingId]) VALUES (0, 'beast:TrendNet','Trendnet',null, 1,0)");
                        context.ExecuteCommand("SET IDENTITY_INSERT [Sensor] OFF");
                        trans.Complete();
                    }
                    //only insert the snort configuration if it has not been done before
                    this.InsertSnortConfiguration();
                    _giltSetupLogger.Info(String.Format("Sensor Created"));
                }                
            }
        }

        private void InsertSnortConfiguration()
        {
            _classificationConfig.WriteToDatabase();
            _referenceConfig.WriteToDatabase();
            _generatorMapConfig.WriteToDatabase();
            _snortMsgMapConfig.WriteToDatabase();
        }
    }
}
