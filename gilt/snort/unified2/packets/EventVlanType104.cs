using MiscUtil.IO;
using System.Runtime.Serialization;

namespace gilt.unified2.packets
{
    /// <summary>
    /// <see href="http://manual.snort.org/node44.html">IPV4 Snort VLAN Event</see>
    /// Configure snort with unified2 logging with either mpls_event_types 
    /// or vlan_event_types to get this record type. 
    /// </summary>    
    [DataContract(Name = "PacketVlanType104", Namespace = "http://www.gilty.com")]
    sealed public class EventVlanType104 : EventCommon
    {
        [DataMember(Name = "IpSourceV4")]
        private uint _ipSourceV4;
        [DataMember(Name = "IpDestinationV4")]
        private uint _ipDestinationV4;
        [DataMember(Name = "SourcePort")]
        private ushort _sourcePort;
        [DataMember(Name = "DestinationPort")]
        private ushort _destinationPort;
        [DataMember(Name = "Protocol")]
        private byte _protocol;
        [DataMember(Name = "ImpactFlag")]
        private byte _impactFlag;
        [DataMember(Name = "Impact")]
        private byte _impact;
        [DataMember(Name = "Blocked")]
        private byte _blocked;
        [DataMember(Name = "MplsLabel")]
        private uint _mplsLabel;
        [DataMember(Name = "VlanId")]
        private ushort _vlanId;
        [DataMember(Name = "PolicyId")]
        private ushort _policyId;

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
        /// Contains the source port of the alerting packet. 
        /// </summary>
        public ushort SourcePort
        {
            get { return _sourcePort; }
        }
        /// <summary>
        /// Contains the destination port of the alerting packet. 
        /// </summary>
        public ushort DestinationPort
        {
            get { return _destinationPort; }
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
        /// <summary>
        /// The extracted mpls label from the mpls header in the alerting packet. 
        /// </summary>
        public uint MplsLabel
        {
            get { return _mplsLabel; }
        }
        /// <summary>
        /// The extracted vlan id from the vlan header in the alerting packet. 
        /// </summary>
        public ushort VlanId
        {
            get { return _vlanId; }
        }
        /// <summary>
        /// Vlan Policy or padding.
        /// </summary>
        public ushort PolicyId
        {
            get { return _policyId; }
        }
        #endregion

        /// <summary>
        /// Create a Unified2 Type 104 Packet
        /// </summary>
        /// <param name="EndianReader">Endian aware binary reader</param>
        /// <param name="Unifed2Header">Unified2Header that defines the packet length and type</param>
        public EventVlanType104(EndianBinaryReader EndianReader, Unified2Header Unifed2Header)
            : base(EndianReader, Unifed2Header)
        {
            _ipSourceV4 = _binaryReader.ReadUInt32();
            _ipDestinationV4 = _binaryReader.ReadUInt32();
            _sourcePort = _binaryReader.ReadUInt16();
            _destinationPort = _binaryReader.ReadUInt16();
            _protocol = _binaryReader.ReadByte();
            _impactFlag = _binaryReader.ReadByte();
            _impact = _binaryReader.ReadByte();
            _blocked = _binaryReader.ReadByte();
            _mplsLabel = _binaryReader.ReadUInt32();
            _vlanId = _binaryReader.ReadUInt16();
            _policyId = _binaryReader.ReadUInt16();
        }
    }
}
