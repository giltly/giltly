using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.logs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqLogHistoryRepository : GenericRepository<LogHistoryProxy>, ILogHistoryRepository
    {
        public MoqLogHistoryRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<LogHistoryProxy>)new List<LogHistoryProxy>
            {
                new LogHistoryProxy( new LogHistory())
            }.AsEnumerable<LogHistoryProxy>().AsQueryable<LogHistoryProxy>();
        }

        public LogHistoryProxy GetEventsByPK(int SensorId, int EventId)
        {
            return new LogHistoryProxy(new LogHistory());
        }

        #region IGenericRepository virtuals
        public override IEnumerable<LogHistoryProxy> FindBy(Func<LogHistoryProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<LogHistoryProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(LogHistoryProxy proxy)
        {
        }
        public override void Add(LogHistoryProxy proxy)
        {
        }
        public override void Delete(LogHistoryProxy proxy)
        {
        }
        #endregion
    }
}
