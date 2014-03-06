
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Data Proxy
    /// </summary>
    public sealed class DataProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Snort Event Id
        /// </summary>
        public decimal EventId { get; set; }
        /// <summary>
        /// Snort Sensor Id
        /// </summary>
        public decimal SensorId { get; set; }
        /// <summary>
        /// The binary payload
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Create a data proxy from data
        /// </summary>
        /// <param name="D">The data</param>
        public DataProxy(Data D)
            : base()
        {
            if (null != D)
            {
                Id = D.Id;
                EventId = D.EventId;
                SensorId = D.SensorId;
                Payload = D.Payload;
            }
        }
    }
}
