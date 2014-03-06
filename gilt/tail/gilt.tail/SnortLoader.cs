using gilt.snort;
using gilt.util;

namespace gilt.tail
{
    /// <summary>
    /// Process the Snort Configuration Files:
    /// sid-msg.map
    /// gen-msg.map
    /// reference.config
    /// classification.config
    /// </summary>
    public class SnortLoader : IGenericConfigWriter
    {
        private SnortConfigReaderWriter _snortConfigReaderWriter = null;

        /// <summary>
        /// Create a snort loader using the settings from app.config
        /// </summary>
        public SnortLoader()
        {
            _snortConfigReaderWriter = new SnortConfigReaderWriter(
                AppSettings.DatabaseConnectionString,
                AppSettings.SnortClassificationFile,
                AppSettings.SnortReferenceFile,
                AppSettings.SnortSensorFile,
                AppSettings.SnortGeneratorFile);
        }

        /// <summary>
        /// Write the snort configuration settings to the database
        /// </summary>
        public void WriteToDatabase()
        {
            _snortConfigReaderWriter.WriteToDatabase();
        }
    }
}
