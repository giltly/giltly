using gilt.dblinq.proxy;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// UdpHeader Repository
    /// </summary>
    public interface IUdpHeadersRepository : IGenericRepository<UdpHeaderProxy>
    {
        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>IcmpHeader</returns>
        UdpHeaderProxy GetEventsByPK(int sensorId, int eventId);
    }
}
