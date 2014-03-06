using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.ipdata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqIcmpHeaderRepository : GenericRepository<IcmpHeaderProxy>, IIcmpHeadersRepository
    {
        public MoqIcmpHeaderRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<IcmpHeaderProxy>)new List<IcmpHeaderProxy>
            {
                new IcmpHeaderProxy( new ICMPHeader())
            }.AsEnumerable<IcmpHeaderProxy>().AsQueryable<IcmpHeaderProxy>();
        }

        public IcmpHeaderProxy GetEventsByPK(int SensorId, int EventId)
        {
            return new IcmpHeaderProxy(new ICMPHeader());
        }

        #region IGenericRepository virtuals
        public override IEnumerable<IcmpHeaderProxy> FindBy(Func<IcmpHeaderProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<IcmpHeaderProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(IcmpHeaderProxy proxy)
        {
        }
        public override void Add(IcmpHeaderProxy proxy)
        {
        }
        public override void Delete(IcmpHeaderProxy proxy)
        {
        }
        #endregion
    }
}
