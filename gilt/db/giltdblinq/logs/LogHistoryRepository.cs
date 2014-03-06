using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.logs
{
    /// <summary>
    /// LogHistory Repository
    /// </summary>
    public sealed class LogHistoryRepository : GenericRepository<LogHistoryProxy>, ILogHistoryRepository
    {
        /// <summary>
        /// Initialize the LogHistory Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.LogHistories select new LogHistoryProxy(u));
        }

        #region ILogHistoryRepository CRUD
        /// <summary>
        /// Update a LogHistory
        /// </summary>
        /// <param name="LogHistoryProxy">LogHistory to Update</param>
        public override void Update(LogHistoryProxy LogHistoryProxy)
        {
            LogHistory existingLogEntry = (from s in DataContext.LogHistories where s.Id == LogHistoryProxy.Id select s).Single();
            existingLogEntry.CurrentLogEntryId = LogHistoryProxy.CurrentLogEntryId;
            existingLogEntry.CurrentOffsetBytes = LogHistoryProxy.CurrentOffsetBytes;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a LogHistory
        /// </summary>
        /// <param name="LogHistoryProxy">LogHistory to Add</param>
        public override void Add(LogHistoryProxy LogHistoryProxy)
        {
            LogHistory newLogHistory = new LogHistory();
            newLogHistory.CurrentLogEntryId = LogHistoryProxy.CurrentLogEntryId;
            newLogHistory.CurrentOffsetBytes = LogHistoryProxy.CurrentOffsetBytes;            
            DataContext.LogHistories.InsertOnSubmit(newLogHistory);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a LogHistory
        /// </summary>
        /// <param name="LogHistoryProxy">LogHistory to Delete</param>
        public override void Delete(LogHistoryProxy LogHistoryProxy)
        {
            LogHistory loghistory = (from s in DataContext.LogHistories where s.Id == LogHistoryProxy.Id select s).Single();
            DataContext.LogHistories.DeleteOnSubmit(loghistory);
            DataContext.SubmitChanges();
        }
        #endregion
    }
}
