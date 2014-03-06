﻿using gilt.dblinq.proxy;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// TcpHeader Repository
    /// </summary>
    public interface ITcpHeadersRepository : IGenericRepository<TcpHeaderProxy>
    {
        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>IcmpHeader</returns>
        TcpHeaderProxy GetEventsByPK(int sensorId, int eventId);
    }
}