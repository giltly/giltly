using System.Threading;
using NLog;
using gilt.geo;
using gilt.unified2;
using gilt.tail.logging;
using gilt.util;

namespace gilt.tail
{
    /// <summary>
    /// The entry point for the tail service
    /// </summary>
    public class ServiceEntry
    {        
        private Logger _giltSetupogger = LogManager.GetLogger("giltsetup");

        /// <summary>
        /// Start method that can be used to start process snort events in a new thread
        /// </summary>
        public void Start()
        {
            // the following takes around 20 minutes
            _giltSetupogger.Info("Inserting Gelocation Data... this will take some time");
            GeoIpParse gi = new GeoIpParse();
            gi.WriteToDatabase();
            _giltSetupogger.Info("Finish - Inserting Gelocation Data");

            _giltSetupogger.Info("Start - Reading Snort Configurations");
            SnortLoader sl = new SnortLoader();
            sl.WriteToDatabase();
            _giltSetupogger.Info("Finish - Snort Configurations");

            //wake up the singleton before starting to log packets
            _giltSetupogger.Info("Waking Up Geolocation");
            Ip2Location.Instance.GetType();
            _giltSetupogger.Info("GeoLocation Online");

            _giltSetupogger.Info("Setting Up Unified Snort Log");
            Unified2LogWatcher slw = new Unified2LogWatcher();
            Unified2LogController lc = new Unified2LogController(slw);
            slw.StartWatching();
            _giltSetupogger.Info("Watch Unified Snort Log");

            //sleep forever
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
