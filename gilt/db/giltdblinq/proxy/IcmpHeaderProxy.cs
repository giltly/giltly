
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// ICMPHeader Proxy
    /// <see href="http://www.iana.org/assignments/icmp-parameters/icmp-parameters.xhtml"/>
    /// </summary>
    public sealed class IcmpHeaderProxy : ProxyBase
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
        /// Type Number
        /// </summary>        
        public byte? Type { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public byte? Code { get; set; }
        /// <summary>
        /// Checksum
        /// </summary>
        public int? Checksum { get; set; }
        /// <summary>
        /// ICMP id
        /// </summary>
        public int? ICMPId { get; set; }
        /// <summary>
        /// ICMP Sequence
        /// </summary>
        public int? ICMPSequence { get; set; }

        /// <summary>
        /// Create an icmpheaderproxy from a ICMPHeader
        /// </summary>
        /// <param name="I">The ICMPHeader</param>
        public IcmpHeaderProxy(ICMPHeader I)            
        {
            if (null != I)
            {
                Id = I.Id;
                EventId = I.EventId;
                SensorId = I.SensorId;
                Type = I.Type;
                Code = I.Code;
                Checksum = I.Checksum;
                ICMPId = I.ICMPId;
                ICMPSequence = I.ICMPSequence;
            }
        }

    }
}
