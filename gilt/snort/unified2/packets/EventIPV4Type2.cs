using MiscUtil.IO;
using System.Runtime.Serialization;

namespace gilt.unified2.packets
{
    /// <summary>
    /// IPV4 Snort Event Type 2 <see href="http://manual.snort.org/node44.html"/>
    /// </summary>
    [DataContract(Name = "IPV4EventType2", Namespace = "http://www.gilty.com")]
    public class EventIPV4Type2 : EventCommon
    {
        [DataMember(Name = "IPSourceV4")]
        private uint _ipSourceV4;
        [DataMember(Name = "IPDestinationV4")]
        private uint _ipDestinationV4;
        [DataMember(Name = "SourcePortIcmpType")]
        private ushort _sourcePortIcmpType;
        [DataMember(Name = "DestinationPortIcmpCode")]
        private ushort _destinationPortIcmpCode;
        [DataMember(Name = "Protocol")]
        private byte _protocol;
        [DataMember(Name = "ImpactFlag")]
        private byte _impactFlag;
        [DataMember(Name = "Impact")]
        private byte _impact;
        [DataMember(Name = "Blocked")]
        private byte _blocked;

        #region Public Properties
        /// <summary>
        /// Source IP of the packet that generated the event. 
        /// </summary>
        public uint IPSourceV4
        {
            get { return _ipSourceV4; }
        }
        /// <summary>
        /// Destination IP of the packet that generated the event. 
        /// </summary>
        public uint IPDestinationV4
        {
            get { return _ipDestinationV4; }
        }
        /// <summary>
        ///  If Protocol is ICMP than this field contains the ICMP type of the alerting packet. 
        /// </summary>
        public ushort SourcePortIcmpType
        {
            get { return _sourcePortIcmpType; }
        }
        /// <summary>
        ///  If protocol is icmp than this field contains the icmp code of the alerting packet. 
        /// </summary>
        public ushort DestinationPortIcmpCode
        {
            get { return _destinationPortIcmpCode; }
        }
        /// <summary>
        /// Transport protocol of the alerting packet. One of: ip, tcp, udp, or icmp. 
        /// </summary>
        public byte Protocol
        {
            get { return _protocol; }
        }
        /// <summary>
        /// Legacy field, specifies whether a packet was dropped or not. 
        /// 32 - Blocked
        /// </summary>
        public byte ImpactFlag
        {
            get { return _impactFlag; }
        }
        /// <summary>
        /// Unused
        /// </summary>
        public byte Impact
        {
            get { return _impact; }
        }
        /// <summary>
        /// Whether the packet was not dropped, was dropped or would have been dropped. 
        ///  0 - Was NOT Dropped 
        ///  1 - Was Dropped 
        ///  2 - Would Have Dropped
        /// </summary>
        public byte Blocked
        {
            get { return _blocked; }
        }
        #endregion

        /// <summary>
        /// Create a Unified2 Type 2 Packet
        /// </summary>
        /// <param name="EndianReader">Endian aware binary reader</param>
        /// <param name="Unifed2Header">Unified2Header that defines the packet length and type</param>
        public EventIPV4Type2(EndianBinaryReader EndianReader, Unified2Header Unifed2Header)
            : base(EndianReader, Unifed2Header)        
        {
            _ipSourceV4 = _binaryReader.ReadUInt32();
            _ipDestinationV4 = _binaryReader.ReadUInt32();
            _sourcePortIcmpType = _binaryReader.ReadUInt16();
            _destinationPortIcmpCode = _binaryReader.ReadUInt16();
            _protocol = _binaryReader.ReadByte();
            _impactFlag = _binaryReader.ReadByte();
            _impact = _binaryReader.ReadByte();
            _blocked = _binaryReader.ReadByte();
        }
    }
}
