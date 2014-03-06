using System;

namespace gilt.dblinq.proxy
{
    /// <summary>
    /// LogHistory proxy
    /// </summary>
    public sealed class LogHistoryProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Current Log Entry Id
        /// </summary>
        public int? CurrentLogEntryId { get; set; }
        /// <summary>
        /// Current Offset Bytes
        /// </summary>
        public int? CurrentOffsetBytes { get; set; }

        /// <summary>
        /// Create a LogHistoryProxy from a LogHistory
        /// </summary>
        /// <param name="L">The LogHistory</param>
        public LogHistoryProxy(LogHistory L)
        {
            if (null != L)
            {
                Id = L.Id;
                CurrentLogEntryId = L.CurrentLogEntryId;
                CurrentOffsetBytes = L.CurrentOffsetBytes;
            }
        }
    }
}
