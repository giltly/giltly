using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqEventCommentRepository : GenericRepository<EventCommentsProxy>, IEventCommentsRepository
    {
        public MoqEventCommentRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<EventCommentsProxy>)new List<EventCommentsProxy>
            {
                new EventCommentsProxy( new EventComment())
            }.AsEnumerable<EventCommentsProxy>().AsQueryable<EventCommentsProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<EventCommentsProxy> FindBy(Func<EventCommentsProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<EventCommentsProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(EventCommentsProxy proxy)
        {
        }
        public override void Add(EventCommentsProxy proxy)
        {
        }
        public override void Delete(EventCommentsProxy proxy)
        {
        }
        #endregion
    }
}
