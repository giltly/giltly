using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.ipdata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqUdpHeaderRepository : GenericRepository<UdpHeaderProxy>, IUdpHeadersRepository
    {
        public MoqUdpHeaderRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<UdpHeaderProxy>)new List<UdpHeaderProxy>
            {
                new UdpHeaderProxy( new UDPHeader())
            }.AsEnumerable<UdpHeaderProxy>().AsQueryable<UdpHeaderProxy>();
        }

        public UdpHeaderProxy GetEventsByPK(int SensorId, int EventId)
        {
            return new UdpHeaderProxy(new UDPHeader());
        }

        #region IGenericRepository virtuals
        public override IEnumerable<UdpHeaderProxy> FindBy(Func<UdpHeaderProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<UdpHeaderProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(UdpHeaderProxy proxy)
        {
        }
        public override void Add(UdpHeaderProxy proxy)
        {
        }
        public override void Delete(UdpHeaderProxy proxy)
        {
        }
        #endregion
    }
}
