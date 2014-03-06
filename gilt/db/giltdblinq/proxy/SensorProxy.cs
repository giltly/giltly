namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Sensor Proxy
    /// </summary>
    public sealed class SensorProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public decimal Id { get; set; }
        /// <summary>
        /// Hostname running the sensor
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Interface name
        /// </summary>
        public string Interface { get; set; }
        /// <summary>
        /// Filtering
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// Detail Id
        /// </summary>
        public int? DetailId { get; set; }
        /// <summary>
        /// Encoding Id
        /// </summary>
        public int? EncodingId { get; set; }
        /// <summary>
        /// Detail string
        /// </summary>
        public string DetailString { get; set; }
        /// <summary>
        /// Encoding String
        /// </summary>
        public string EncodingString { get; set; }        
        /// <summary>
        /// Events count for the sensor
        /// </summary>
        public int EventCount { get; set; }

        /// <summary>
        /// Create a SensorProxy from a Sensor
        /// </summary>
        /// <param name="S">The Sensor</param>
        public SensorProxy(Sensor S)            
        {
            if (null != S)
            {
                Id = S.Id;
                HostName = S.Hostname;
                Interface = S.Interface;
                Filter = S.Filter;
                DetailId = S.DetailId;
                EncodingId = S.EncodingId;
            }
        }
    }
}

