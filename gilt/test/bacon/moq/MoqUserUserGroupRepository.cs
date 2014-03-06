using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.user;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqUserUserGroupRepository : GenericRepository<UserUserGroupProxy>, IUserUserGroupsRepository
    {
        public MoqUserUserGroupRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<UserUserGroupProxy>)new List<UserUserGroupProxy>
            {
                new UserUserGroupProxy( new UserUserGroup())
            }.AsEnumerable<UserUserGroupProxy>().AsQueryable<UserUserGroupProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<UserUserGroupProxy> FindBy(Func<UserUserGroupProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<UserUserGroupProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(UserUserGroupProxy proxy)
        {
        }
        public override void Add(UserUserGroupProxy proxy)
        {
        }
        public override void Delete(UserUserGroupProxy proxy)
        {
        }
        #endregion
    }
}
