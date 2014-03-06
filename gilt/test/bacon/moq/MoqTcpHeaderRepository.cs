using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.ipdata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqTcpHeaderRepository : GenericRepository<TcpHeaderProxy>, ITcpHeadersRepository
    {
        public MoqTcpHeaderRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<TcpHeaderProxy>)new List<TcpHeaderProxy>
            {
                new TcpHeaderProxy( new TCPHeader())
            }.AsEnumerable<TcpHeaderProxy>().AsQueryable<TcpHeaderProxy>();
        }

        public TcpHeaderProxy GetEventsByPK(int SensorId, int EventId)
        {
            return new TcpHeaderProxy(new TCPHeader());
        }

        #region IGenericRepository virtuals
        public override IEnumerable<TcpHeaderProxy> FindBy(Func<TcpHeaderProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<TcpHeaderProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(TcpHeaderProxy proxy)
        {
        }
        public override void Add(TcpHeaderProxy proxy)
        {
        }
        public override void Delete(TcpHeaderProxy proxy)
        {
        }
        #endregion
    }
}
