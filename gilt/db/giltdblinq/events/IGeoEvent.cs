using gilt.dblinq.proxy;
using System.Collections.Generic;

namespace gilt.dblinq.events
{
    /// <summary>
    /// GeoLocation Methods
    /// </summary>
    public interface IGeoEvent
    {
        /// <summary>
        /// Get Source and Destination Locations
        /// </summary>
        /// <returns></returns>
        IEnumerable<GeoDataProxy> GetGeoPaths();
    }
}
