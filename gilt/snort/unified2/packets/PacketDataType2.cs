using MiscUtil.IO;
using System.Runtime.Serialization;

namespace gilt.unified2.packets
{
    /// <summary>
    /// <see href="http://manual.snort.org/node44.html">Snort Type 2 Event -- Raw packet data</see>
    /// </summary>
    [DataContract(Name = "PacketDataType2", Namespace = "http://www.gilty.com")]
    sealed public class PacketDataType2 : PacketBase
    {
        [DataMember(Name = "SensorId")]
        private uint _sensorId;
        [DataMember(Name = "EventId")]
        private uint _eventId;
        [DataMember(Name = "EventSecond")]
        private uint _eventSecond;
        [DataMember(Name = "PacketSecond")]
        private uint _packetSecond;
        [DataMember(Name = "PacketMicroSecond")]
        private uint _packetMicroSecond;
        [DataMember(Name = "LinkType")]
        private uint _linkType;
        [DataMember(Name = "PacketLength")]
        private uint _packetLength;
        [DataMember(Name = "PacketData")]
        private byte[] _packetData;

        #region Public Properties
        /// <summary>
        /// The id of the sensor defined in app.config
        /// </summary>
        public uint SensorId
        {
            get { return _sensorId; }
        }
        /// <summary>
        /// The Snort Event Id. The Event ID field is used to facilitate the task of coalescing events with packet data. 
        /// </summary>
        public uint EventId
        {
            get { return _eventId; }
        }
        /// <summary>
        /// Timestamp represented as seconds since the epoch of when the alert was generated. 
        /// </summary>
        public uint EventSecond
        {
            get { return _eventSecond; }
        }
        /// <summary>
        /// Timestamp represented as seconds since the epoch of when the packet was received. 
        /// </summary>
        public uint PacketSecond
        {
            get { return _packetSecond; }
        }
        /// <summary>
        /// Timestamp represented as micro seconds since the epoch of when the packet was received. 
        /// </summary>
        public uint PacketMicroSecond
        {
            get { return _packetMicroSecond; }
        }
        /// <summary>
        /// The Datalink type of the packet, typically EN10M but could be any of the values as returned by pcap_datalink that Snort handles. 
        /// </summary>
        public uint LinkType
        {
            get { return _linkType; }
        }
        /// <summary>
        /// Length of the Packet Data in bytes 
        /// </summary>
        public uint PacketLength
        {
            get { return _packetLength; }
        }
        /// <summary>
        /// The raw packet data
        /// </summary>
        public byte[] PacketData
        {
            get { return _packetData; }
        }
        #endregion

        /// <summary>
        /// Construct a Generic Snort Event using an endian enabled binary reader and a Unified2Header
        /// </summary>
        /// <param name="BinaryRead">Endian aware binary reader</param>
        /// <param name="UnifiedHead">Unified2Header that defines the packet length and type</param>
        public PacketDataType2(EndianBinaryReader BinaryRead, Unified2Header UnifiedHead)
            : base(BinaryRead, UnifiedHead)
        {
            _sensorId = _binaryReader.ReadUInt32();
            _eventId = _binaryReader.ReadUInt32();
            _eventSecond = _binaryReader.ReadUInt32();
            _packetSecond = _binaryReader.ReadUInt32();
            _packetMicroSecond = _binaryReader.ReadUInt32();
            _linkType = _binaryReader.ReadUInt32();
            _packetLength = _binaryReader.ReadUInt32();
            _packetData = _binaryReader.ReadBytes((int)_packetLength);
        }
    }
}