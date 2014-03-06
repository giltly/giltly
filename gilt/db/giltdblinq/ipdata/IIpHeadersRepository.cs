using gilt.dblinq.proxy;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// IPHeader Repository
    /// </summary>
    public interface IIPHeadersRepository : IGenericRepository<IpHeaderProxy>
    {
        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>IcmpHeader</returns>
        IpHeaderProxy GetEventsByPK(int SensorId, int EventId);
    }
}
