using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.logs
{
    /// <summary>
    /// LogEntry Repository
    /// </summary>
    public sealed class LogEntryRepository : GenericRepository<LogEntryProxy>, ILogEntryRepository
    {
        /// <summary>
        /// Initialize the LogEntry Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.LogEntries select new LogEntryProxy(u));
        }

        #region ILogEntryRepository CRUD
        /// <summary>
        /// Update a LogEntry
        /// </summary>
        /// <param name="LogEntryProxy">LogEntry to Update</param>
        public override void Update(LogEntryProxy LogEntryProxy)
        {
            LogEntry existingLogEntry = (from s in DataContext.LogEntries where s.Id == LogEntryProxy.Id select s).Single();
            existingLogEntry.FileName = LogEntryProxy.FileName;
            existingLogEntry.CreatedOn = LogEntryProxy.CreatedOn;
            existingLogEntry.ModifiedOn = LogEntryProxy.ModifiedOn;
            existingLogEntry.SizeBytes = LogEntryProxy.SizeBytes;
            existingLogEntry.StartedOn = LogEntryProxy.StartedOn;
            existingLogEntry.FinishedOn = LogEntryProxy.FinishedOn;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a LogEntry
        /// </summary>
        /// <param name="LogEntryProxy">LogEntry to Add</param>
        public override void Add(LogEntryProxy LogEntryProxy)
        {
            LogEntry newLogEntry = new LogEntry();
            newLogEntry.FileName = LogEntryProxy.FileName;
            newLogEntry.CreatedOn = LogEntryProxy.CreatedOn;
            newLogEntry.ModifiedOn = LogEntryProxy.ModifiedOn;
            newLogEntry.SizeBytes = LogEntryProxy.SizeBytes;
            newLogEntry.StartedOn = LogEntryProxy.StartedOn;
            newLogEntry.FinishedOn = LogEntryProxy.FinishedOn;
            DataContext.LogEntries.InsertOnSubmit(newLogEntry);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a LogEntry
        /// </summary>
        /// <param name="LogEntryProxy">LogEntry to Delete</param>
        public override void Delete(LogEntryProxy LogEntryProxy)
        {
            LogEntry logentry = (from s in DataContext.LogEntries where s.Id == LogEntryProxy.Id select s).Single();
            DataContext.LogEntries.DeleteOnSubmit(logentry);
            DataContext.SubmitChanges();
        }
        #endregion
    }
}
