using System.Collections.Generic;
using gilt.dblinq.proxy;

namespace gilt.dblinq.events
{
    /// <summary>
    /// Event delete data
    /// </summary>
    public interface IEventDelete
    {
        /// <summary>
        /// Delete all event data
        /// </summary>
        void DeleteAllEventData();
        /// <summary>
        /// Delete all geolocation data
        /// </summary>
        void DeleteAllGeoData();
        /// <summary>
        /// Delete all snort data
        /// </summary>
        void DeleteAllSnortData();
        /// <summary>
        /// Delete all log history data
        /// </summary>
        void DeleteAllLogHistoryData();
        /// <summary>
        /// Delete all non geolocation data
        /// </summary>
        void DeleteAllNonGeoData();
        /// <summary>
        /// Delete everything
        /// </summary>
        void DeleteAllData();        
    }
}
