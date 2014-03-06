using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.user;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqUserGroupRepository : GenericRepository<UserGroupsProxy>, IUserGroupsRepository
    {
        public MoqUserGroupRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<UserGroupsProxy>)new List<UserGroupsProxy>
            {
                new UserGroupsProxy( new UserGroup())
            }.AsEnumerable<UserGroupsProxy>().AsQueryable<UserGroupsProxy>();
        }

        public IEnumerable<UserGroupsProxy> GetUnassignedUserGroups(int UserId)
        {
            return _genericQuery.Where(x=> true).AsEnumerable();
        }
        public IEnumerable<UserGroupsProxy> GetAssignedUserGroups(int UserId)
        {
            return _genericQuery.Where(x=> true).AsEnumerable();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<UserGroupsProxy> FindBy(Func<UserGroupsProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<UserGroupsProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(UserGroupsProxy proxy)
        {
        }
        public override void Add(UserGroupsProxy proxy)
        {
        }
        public override void Delete(UserGroupsProxy proxy)
        {
        }
        #endregion
    }
}
