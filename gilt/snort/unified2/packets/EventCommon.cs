using MiscUtil.IO;
using System.Runtime.Serialization;

namespace gilt.unified2.packets
{    
    /// <summary>    
    /// Common Snort Event Data <see href="http://manual.snort.org/node44.html" />
    /// </summary>    
    [DataContract(Name = "EventCommon", Namespace = "http://www.gilty.com")]   
    public abstract class EventCommon : PacketBase
    {
        #pragma warning disable 1591
        [DataMember(Name = "SensorId")]
        protected uint _sensorId;
        [DataMember(Name = "EventId")]
        protected uint _eventId;
        [DataMember(Name = "EventSecond")]
        protected uint _eventSecond;
        [DataMember(Name = "EventMiroSecond")]
        protected uint _eventMicroSecond;
        [DataMember(Name = "SignatureId")]
        protected uint _signatureId;
        [DataMember(Name = "GeneratorId")]
        protected uint _generatorId;
        [DataMember(Name = "SignatureRevision")]
        protected uint _signatureRevision;
        [DataMember(Name = "ClassificationId")]
        protected uint _classificationId;
        [DataMember(Name = "PriorityId")]
        protected uint _priorityId;
        #pragma warning restore 1591

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
        /// Timestamp represented as micro seconds since the epoch of when the alert was generated. 
        /// </summary>
        public uint EventMiroSecond
        {
            get { return _eventMicroSecond; }
        }
        /// <summary>
        /// The Signature ID of the alerting rule, as specified by the sid keyword. 
        /// </summary>
        public uint SignatureId
        {
            get { return _signatureId; }
        }
        /// <summary>
        /// The Generator ID of the alerting rule, as specified by the gid keyword. 
        /// </summary>
        public uint GeneratorId
        {
            get { return _generatorId; }
        }
        /// <summary>
        /// Revision of the rule as specified by the rev keyword. 
        /// </summary>
        public uint SignatureRevision
        {
            get { return _signatureRevision; }
        }
        /// <summary>
        /// Classification ID as mapped in the file classifications.conf 
        /// </summary>
        public uint ClassificationId
        {
            get { return _classificationId; }
        }
        /// <summary>
        /// Priority of the rule as mapped in the file classifications.conf or overridden by the priority keyword for text rules. 
        /// </summary>
        public uint PriorityId
        {
            get { return _priorityId; }
        }
        #endregion

        /// <summary>
        /// Construct a Generic Snort Event using an endian enabled binary reader and a Unified2Header
        /// </summary>
        /// <param name="BinaryRead">Endian aware binary reader</param>
        /// <param name="UnifiedHead">Unified2Header that defines the packet length and type</param>
        public EventCommon(EndianBinaryReader BinaryRead, Unified2Header UnifiedHead)
            : base(BinaryRead, UnifiedHead)
        {
            _sensorId = _binaryReader.ReadUInt32();
            _eventId = _binaryReader.ReadUInt32();
            _eventSecond = _binaryReader.ReadUInt32();
            _eventMicroSecond = _binaryReader.ReadUInt32();
            _signatureId = _binaryReader.ReadUInt32();
            _generatorId = _binaryReader.ReadUInt32();
            _signatureRevision = _binaryReader.ReadUInt32();
            _classificationId = _binaryReader.ReadUInt32();
            _priorityId = _binaryReader.ReadUInt32();
        }
    }
}
