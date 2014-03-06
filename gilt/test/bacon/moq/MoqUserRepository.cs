using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.user;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqUserRepository : GenericRepository<UsersProxy>, IUsersRepository
    {
        public MoqUserRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<UsersProxy>)new List<UsersProxy>
            {
                new UsersProxy( new User())
            }.AsEnumerable<UsersProxy>().AsQueryable<UsersProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<UsersProxy> FindBy(Func<UsersProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<UsersProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(UsersProxy proxy)
        {
        }
        public override void Add(UsersProxy proxy)
        {
        }
        public override void Delete(UsersProxy proxy)
        {
        }
        #endregion

        public UsersProxy GetUserByEmail(string Email)
        {
            return new UsersProxy(new User());
        }
    }
}
