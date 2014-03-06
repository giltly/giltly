using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.logs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqLogEntryRepository : GenericRepository<LogEntryProxy>, ILogEntryRepository
    {
        public MoqLogEntryRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<LogEntryProxy>)new List<LogEntryProxy>
            {
                new LogEntryProxy( new LogEntry())
            }.AsEnumerable<LogEntryProxy>().AsQueryable<LogEntryProxy>();
        }

        public LogEntryProxy GetEventsByPK(int SensorId, int EventId)
        {
            return new LogEntryProxy(new LogEntry());
        }

        #region IGenericRepository virtuals
        public override IEnumerable<LogEntryProxy> FindBy(Func<LogEntryProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<LogEntryProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(LogEntryProxy proxy)
        {
        }
        public override void Add(LogEntryProxy proxy)
        {
        }
        public override void Delete(LogEntryProxy proxy)
        {
        }
        #endregion
    }
}
