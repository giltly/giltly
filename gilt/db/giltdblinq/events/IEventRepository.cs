using System;
using System.Linq;
using System.Collections.Generic;
using gilt.dblinq.proxy;

namespace gilt.dblinq.events
{
    /// <summary>
    /// Event Repository
    /// </summary>
    public interface IEventRepository : IGenericRepository<EventProxy>, IGeoEvent, IEventDelete
    {
        /// <summary>
        /// Get Event by Primary Key
        /// </summary>
        /// <param name="SensorId">The sensor Id</param>
        /// <param name="EventId">The event Id</param>
        /// <returns>List of events</returns>
        EventProxy GetEventsByPK(int SensorId, int EventId);
        /// <summary>
        /// Get Events by Ip Address
        /// </summary>
        /// <returns>List of events grouped by Ip</returns>
        IEnumerable<EventsByIpProxy> GetEventsByIp();
        /// <summary>
        /// Get Events by Country
        /// </summary>
        /// <returns>List of events grouped by Country</returns>
        IEnumerable<EventsByCountryProxy> GetEventsByCountry();
        /// <summary>
        /// Get Events by Destination Port
        /// </summary>
        /// <returns>List of events grouped by detination port</returns>
        IEnumerable<UniqueDestinationPortProxy> GetEventsByDestinationPort();
        /// <summary>
        /// Get Events by Source Port
        /// </summary>
        /// <returns>List of events grouped by source port</returns>
        IEnumerable<UniqueSourcePortProxy> GetEventsBySourcePort();
    }
}
