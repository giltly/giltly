using System.Data.Linq;
using System.Net;

namespace gilt.dblinq.proxy
{
    /// <summary>
    /// IpV4 Header
    /// <see href="http://en.wikipedia.org/wiki/IPv4_header#Header"/>
    /// </summary>
    public sealed class IpHeaderProxy : ProxyBase
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
        /// IP Source binary
        /// </summary>
        public Binary IPSource { get; set; }
        /// <summary>
        /// Ip Destination binary
        /// </summary>
        public Binary IPDestination { get; set; }
        /// <summary>
        /// Ip Source dotted decimal
        /// </summary>
        public string IpSourceString { get; set; }
        /// <summary>
        /// Ip Destination dotted decimal
        /// </summary>
        public string IpDestinationString { get; set; }
        /// <summary>
        /// Ip source GeoLocation Id
        /// </summary>
        public int? IPSourceLocationId { get; set; }
        /// <summary>
        /// Ip destination GeoLocation Id
        /// </summary>
        public int? IPDestinationLocationId { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public byte? Version { get; set; }
        /// <summary>
        /// Header Length
        /// </summary>
        public byte? HeaderLength { get; set; }
        /// <summary>
        /// Type of Service
        /// </summary>
        public byte? TOS { get; set; }
        /// <summary>
        /// Header length in bytes
        /// </summary>
        public int? Length { get; set; }
        /// <summary>
        /// Identification
        /// </summary>
        public int? Identification { get; set; }
        /// <summary>
        /// Flags
        /// </summary>
        public byte? Flags { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        public int? Offset { get; set; }
        /// <summary>
        /// TTL
        /// </summary>
        public byte? TTL { get; set; }
        /// <summary>
        /// Protocol Id
        /// </summary>
        public byte ProtocolId { get; set; }
        /// <summary>
        /// Checksum
        /// </summary>
        public int? CheckSum { get; set; }

        /// <summary>
        /// Create an IpHeaderProxy from an IPHeader
        /// </summary>
        /// <param name="Iph">The IPHeader</param>
        public IpHeaderProxy(IPHeader Iph) 
        {
            if (null != Iph)
            {
                Id = Iph.Id;
                SensorId = Iph.SensorId;
                EventId = Iph.EventId;
                IPSource = Iph.IPSource;
                IPDestination = Iph.IPDestination;
                //the ipsource and ipdestination addresses are not always filled in
                if (null != IPSource)
                {
                    IpSourceString = new IPAddress(IPSource.ToArray()).ToString();
                }
                if (null != IPDestination)
                {
                    IpDestinationString = new IPAddress(IPDestination.ToArray()).ToString();
                }
                IPSourceLocationId = Iph.IPSourceLocationId;
                IPDestinationLocationId = Iph.IPDestinationLocationId;
                Version = Iph.Version;
                HeaderLength = Iph.HeaderLength;
                TOS = Iph.TOS;
                Length = Iph.Length;
                Identification = Iph.Identification;
                Flags = Iph.Flags;
                Offset = Iph.Offset;
                TTL = Iph.TTL;
                ProtocolId = Iph.ProtocolId;
                CheckSum = Iph.CheckSum;
            }
        }
    }
}
