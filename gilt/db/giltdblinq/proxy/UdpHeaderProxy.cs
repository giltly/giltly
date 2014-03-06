
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// UdpHeader Proxy
    /// <see href="http://en.wikipedia.org/wiki/User_Datagram_Protocol"/>
    /// </summary>
    public sealed class UdpHeaderProxy : ProxyBase
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
        /// Source port number
        /// </summary>
        public int SourcePort { get; set; }
        /// <summary>
        /// Destination port number
        /// </summary>
        public int DestinationPort { get; set; }
        /// <summary>
        /// Length in bytes of the udp packet
        /// </summary>
        public int? Length { get; set; }
        /// <summary>
        /// Packet checksum
        /// </summary>
        public int? CheckSum { get; set; }

        /// <summary>
        /// Create a UdpHeaderProxy from a UDPHeader
        /// </summary>
        /// <param name="U">The UDPHeader</param>
        public UdpHeaderProxy(UDPHeader U)
            : base()
        {
            if (null != U)
            {
                Id = U.Id;
                EventId = U.EventId;
                SensorId = U.SensorId;
                SourcePort = U.SourcePort;
                DestinationPort = U.DestinationPort;
                Length = U.Length;
                CheckSum = U.CheckSum;                
            }
        }

    }
}
