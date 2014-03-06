using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.search;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqSearchRepository : GenericRepository<SearchProxy>, ISearchRepository
    {
        public MoqSearchRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<SearchProxy>)new List<SearchProxy>
            {
                new SearchProxy( new Search())
            }.AsEnumerable<SearchProxy>().AsQueryable<SearchProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<SearchProxy> FindBy(Func<SearchProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<SearchProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(SearchProxy proxy)
        {
        }
        public override void Add(SearchProxy proxy)
        {
        }
        public override void Delete(SearchProxy proxy)
        {
        }
        #endregion

        #region ISearchEvent
        public IEnumerable<int> GetDistinctSourcePorts()
        {
            return new List<int> { 10, 20 };
        }
        public IEnumerable<int> GetDistinctDestinationPorts()
        {
            return new List<int> { 80, 443 };
        }
        public IEnumerable<Binary> GetDistinctSourceIP()
        {
            return new List<Binary> { new byte[] {0}};
        }
        public IEnumerable<Binary> GetDistinctDestinationIP()
        {
            return new List<Binary> { new byte[] { 1 } };
        }
        public IEnumerable<SignatureClassificationProxy> GetSignatureClassifications()
        {
            return new List<SignatureClassificationProxy> { new SignatureClassificationProxy(new SignatureClassification { }) };
        }
        public IEnumerable<SignatureProxy> GetSignatures()
        {
            return new List<SignatureProxy> { new SignatureProxy(new Signature { }) };
        }
        #endregion
    }
}
