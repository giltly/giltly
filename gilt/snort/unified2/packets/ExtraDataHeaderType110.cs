using MiscUtil.IO;
using System.Runtime.Serialization;

namespace gilt.unified2.packets
{
    /// <summary>
    /// <see href="http://manual.snort.org/node44.html">Snort Extra Header Data Event</see>
    /// </summary>
    [DataContract(Name = "ExtraDataHeaderType110", Namespace = "http://www.gilty.com")]
    sealed public class ExtraDataHeaderType110 : PacketBase
    {
        [DataMember(Name = "EventType")]
        private uint _eventType;
        [DataMember(Name = "EventLength")]
        private uint _eventLength;
        [DataMember(Name = "SensorId")]
        private uint _sensorId;
        [DataMember(Name = "EventId")]
        private uint _eventId;
        [DataMember(Name = "EventSecond")]
        private uint _eventSecond;
        [DataMember(Name = "Type")]
        private uint _type;
        [DataMember(Name = "BlobType")]
        private uint _blobType;
        [DataMember(Name = "BlobLength")]
        private uint _blobLength;
        [DataMember(Name = "Data")]
        private byte[] _data;

        #region Public Properties
        /// <summary>
        ///1  Original Client IPv4 
        ///2  Original Client IPv6
        ///3  UNUSED
        ///4  GZIP Decompressed Data
        ///5  SMTP Filename
        ///6  SMTP Mail From
        ///7  SMTP RCPT To
        ///8  SMTP Email Headers
        ///9  HTTP URI
        ///10 HTTP Hostname
        ///11 IPv6 Source Address
        ///12 IPv6 Destination Address
        ///13 Normalized Javascript Data        
        /// </summary>
        public uint EventType
        {
            get { return _eventType; }
        }
        /// <summary>
        /// Length of the data in bytes stored in the extra data record.
        /// </summary>
        public uint EventLength
        {
            get { return _eventLength; }
        }
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
        /// Link Type = EN10M
        /// </summary>
        public uint Type
        {
            get { return _type; }
        }
        /// <summary>
        /// 1 = Blob
        /// </summary>
        public uint BlobType
        {
            get { return _blobType; }
        }
        /// <summary>
        /// The length in bytes of the data
        /// </summary>
        public uint BlobLength
        {
            get { return _blobLength; }
        }
        /// <summary>
        /// The raw packet data
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
        }
        #endregion

        /// <summary>
        /// Create a Unified2 Type 110 Packet
        /// </summary>
        /// <param name="EndianReader">Endian aware binary reader</param>
        /// <param name="Unifed2Header">Unified2Header that defines the packet length and type</param>
        public ExtraDataHeaderType110(EndianBinaryReader EndianReader, Unified2Header Unifed2Header)
            : base(EndianReader, Unifed2Header)        
        { 
            _eventType = EndianReader.ReadUInt32();
            _eventLength = EndianReader.ReadUInt32();
            _sensorId = EndianReader.ReadUInt32();
            _eventId = EndianReader.ReadUInt32();
            _eventSecond = EndianReader.ReadUInt32();
            _type = EndianReader.ReadUInt32();
            _blobType = EndianReader.ReadUInt32();
            /* Length of the data + sizeof(blob_length) + sizeof(data_type)*/
            _blobLength = EndianReader.ReadUInt32() - 8;            
            _data = EndianReader.ReadBytes((int)_blobLength);
        }
    }
}
