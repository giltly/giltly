using System;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace gilt.util
{
    /// <summary>
    /// Static Wrapper Class around App.config
    /// Yields all libraries a static path to the application configuration
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// The full path name of the running assembly
        /// Usefull for unittesting and during application startup
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        /// <summary>
        /// The database connection string
        /// </summary>
        public static string DatabaseConnectionString 
        {
            get
            {
                return _snortSettings.Settings["connection-string"].Value;
            }                
        }
        /// <summary>
        /// The snort log folder
        /// </summary>
        public static string SnortLogFolder 
        {
            get
            {
                return _snortSettings.Settings["log-folder"].Value;
            }                
        }
        /// <summary>
        /// The unified2 log file prefix.
        /// </summary>
        public static string SnortLogPrefixName 
        {
            get
            {
                return _snortSettings.Settings["log-prefixname"].Value;
            }                
        }
        /// <summary>
        /// Snort alert classification File
        /// </summary>
        public static string SnortClassificationFile 
        {
            get
            {
                return _snortSettings.Settings["classification-config"].Value;
            }                
        }
        /// <summary>
        /// The file defineing URLs for the references found in the rules
        /// </summary>
        public static string SnortReferenceFile 
        {
            get
            {
                return _snortSettings.Settings["reference-config"].Value;
            }                
        }
        /// <summary>
        /// he file sid-msg.map contains a mapping of alert messages to Snort rule IDs. 
        /// This information is useful when post-processing alert to map an ID to an alert message. 
        /// </summary>
        public static string SnortSensorFile 
        {
            get
            {
                return _snortSettings.Settings["sid-msg-map"].Value;
            }                
        }
        /// <summary>
        /// Defines what generator is used to identify which part of snort is generating the alert
        /// </summary>
        public static string SnortGeneratorFile
        {
            get
            {
                return _snortSettings.Settings["gen-msg-map"].Value;
            }
        }
        /// <summary>
        /// True, if the site is a demo site
        /// A demo site cannot change any records in the database from the web front end
        /// </summary>
        public static bool IsDemoSite
        {
            get
            {
                //TODO: return from app.config
                return false;
            }
        }

        #region Private Config Properties
        private static Configuration _config
        {
            get
            {
                //load web.config if it exists in the current directory
                ExeConfigurationFileMap configFile = new System.Configuration.ExeConfigurationFileMap();
                string webConfig = Path.Combine(AssemblyDirectory, "web.config");
                if (File.Exists(webConfig))
                {
                    configFile.ExeConfigFilename = webConfig;
                }
                else
                {
                    //otherwise load app.config
                    configFile.ExeConfigFilename = Path.Combine(AssemblyDirectory, System.AppDomain.CurrentDomain.FriendlyName.Replace(".vshost","").Replace(".exe",".exe.config"));
                }
                return System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, System.Configuration.ConfigurationUserLevel.None);
            }
        }
        private static AppSettingsSection _snortSettings
        {
            get { return (AppSettingsSection)_config.GetSection("snort"); }
        }
        #endregion
    }
}
