using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.ipdata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqDataRepository : GenericRepository<DataProxy>, IDatasRepository
    {
        public MoqDataRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<DataProxy>)new List<DataProxy>
            {
                new DataProxy( new Data())
            }.AsEnumerable<DataProxy>().AsQueryable<DataProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<DataProxy> FindBy(Func<DataProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<DataProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(DataProxy proxy)
        {
        }
        public override void Add(DataProxy proxy)
        {
        }
        public override void Delete(DataProxy proxy)
        {
        }
        #endregion
    }
}
