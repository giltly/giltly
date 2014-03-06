using System;

namespace gilt.dblinq.proxy
{
    /// <summary>
    /// LogEntry Proxy
    /// </summary>
    public sealed class LogEntryProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Log filename
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Date Created On
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Date Modfied On
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// Log file size in bytes
        /// </summary>
        public int SizeBytes { get; set; }
        /// <summary>
        /// Log Started At Time
        /// </summary>
        public DateTime? StartedOn { get; set; }
        /// <summary>
        /// Log Finished At Time
        /// </summary>
        public DateTime? FinishedOn { get; set; }

        /// <summary>
        /// Create a LogEntryProxy from a LogEntry
        /// </summary>
        /// <param name="Le">The LogEntry</param>
        public LogEntryProxy(LogEntry Le)            
        {
            if (null != Le)
            {
                Id = Le.Id;
                FileName = Le.FileName;
                CreatedOn = Le.CreatedOn;
                ModifiedOn = Le.ModifiedOn;
                SizeBytes = Le.SizeBytes;
                StartedOn = Le.StartedOn;
                FinishedOn = Le.FinishedOn;
            }
        }
    }
}
