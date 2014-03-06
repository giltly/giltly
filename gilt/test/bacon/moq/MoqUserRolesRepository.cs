using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.roles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqUserRolesRepository : GenericRepository<UserRolesProxy>, IUserRolesRepository
    {
        public MoqUserRolesRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<UserRolesProxy>)new List<UserRolesProxy>
            {
                new UserRolesProxy( new UserRole())
            }.AsEnumerable<UserRolesProxy>().AsQueryable<UserRolesProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<UserRolesProxy> FindBy(Func<UserRolesProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<UserRolesProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(UserRolesProxy proxy)
        {
        }
        public override void Add(UserRolesProxy proxy)
        {
        }
        public override void Delete(UserRolesProxy proxy)
        {
        }
        #endregion
    }
}
