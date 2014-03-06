using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.signatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqSignatureRepository : GenericRepository<SignatureProxy>, ISignatureRepository
    {
        public MoqSignatureRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<SignatureProxy>)new List<SignatureProxy>
            {
                new SignatureProxy( new Signature())
            }.AsEnumerable<SignatureProxy>().AsQueryable<SignatureProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<SignatureProxy> FindBy(Func<SignatureProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<SignatureProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(SignatureProxy proxy)
        {
        }
        public override void Add(SignatureProxy proxy)
        {
        }
        public override void Delete(SignatureProxy proxy)
        {
        }
        #endregion
    }
}
