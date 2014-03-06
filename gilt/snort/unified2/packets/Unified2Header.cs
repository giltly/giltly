
namespace gilt.unified2.packets
{    
    /// <summary>
    /// Defines the record type and length of the data that follows
    /// </summary>
    public sealed class Unified2Header
    {
        private uint _recordType;
        private uint _recordLength;

        #region Public Properties
        /// <summary>
        /// The type of unified 2 event
        /// </summary>
        public uint RecordType
        {
            get { return _recordType;}
        }      
        /// <summary>
        /// The length of the record in bytes
        /// </summary>
        public uint RecordLength
        {
            get { return _recordLength; }
        }
        #endregion

        /// <summary>
        /// Create a unified2 header from type and length
        /// </summary>
        /// <param name="RecordType">The unified2 record type</param>
        /// <param name="RecordLength">The length in bytes of the event</param>
        public Unified2Header(uint RecordType, uint RecordLength)
        {
            _recordType = RecordType;
            _recordLength = RecordLength;
        }
    }
}
