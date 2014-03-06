
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// TCP Header
    /// <see href="http://www.freesoft.org/CIE/Course/Section4/8.htm"/>
    /// </summary>
    public sealed class TcpHeaderProxy : ProxyBase
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
        /// Sequence number
        /// </summary>
        public decimal? Sequence { get; set; }
        /// <summary>
        /// Acknowledgment Number
        /// </summary>
        public decimal? ACK { get; set; }
        /// <summary>
        /// Data Offset
        /// </summary>
        public byte? Offset { get; set; }
        /// <summary>
        /// Reserved. Must be zero
        /// </summary>
        public byte? Reserved { get; set; }
        /// <summary>
        /// Flags
        /// </summary>
        public byte? Flags { get; set; }
        /// <summary>
        /// Window size
        /// </summary>
        public int? Window { get; set; }
        /// <summary>
        /// Packet checksum
        /// </summary>
        public int? CheckSum { get; set; }
        /// <summary>
        /// Urgent
        /// </summary>
        public int? Urgent { get; set; }

        /// <summary>
        /// Create a TcpHeaderProxy from a TCPHeader 
        /// </summary>
        /// <param name="T">The TCPHeader</param>
        public TcpHeaderProxy(TCPHeader T)            
        {
            if (null != T)
            {
                Id = T.Id;
                EventId = T.EventId;
                SensorId = T.SensorId;
                SourcePort = T.SourcePort;
                DestinationPort = T.DestinationPort;
                Sequence = T.Sequence;
                ACK = T.ACK;
                Offset = T.Offset;
                Reserved = T.Reserved;
                Flags = T.Flags;
                Window = T.Window;
                CheckSum = T.CheckSum;
                Urgent = T.Urgent;                
            }
        }

    }
}
