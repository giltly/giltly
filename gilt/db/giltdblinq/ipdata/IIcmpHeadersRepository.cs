using gilt.dblinq.proxy;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// IcmpHeader Repository
    /// </summary>
    public interface IIcmpHeadersRepository : IGenericRepository<IcmpHeaderProxy>
    {
        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>IcmpHeader</returns>
        IcmpHeaderProxy GetEventsByPK(int SensorId, int EventId);
    }
}
