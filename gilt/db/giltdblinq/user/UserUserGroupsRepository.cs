using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.user
{
    /// <summary>
    /// UserUserGroup Repository
    /// </summary>
    public sealed class UserUserGroupsRepository : GenericRepository<UserUserGroupProxy>, IUserUserGroupsRepository
    {
        /// <summary>
        /// Initialize the UserUserGroup Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.UserUserGroups select new UserUserGroupProxy(u));
        }

        #region CRUD
        /// <summary>
        /// Update a UserUserGroup
        /// </summary>
        /// <param name="UserUserGroupProxy">UserUserGroup to Update</param>
        public override void Update(UserUserGroupProxy UserUserGroupProxy)
        {
            UserUserGroup existingUserGroup = (from s in DataContext.UserUserGroups where s.UserGroupId == UserUserGroupProxy.UserGroupId && s.UserId == UserUserGroupProxy.UserId select s).Single();
            existingUserGroup.UserId = UserUserGroupProxy.UserId;
            existingUserGroup.UserGroupId = UserUserGroupProxy.UserGroupId;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a UserUserGroup
        /// </summary>
        /// <param name="UserUserGroupProxy">UserUserGroup to Add</param>
        public override void Add(UserUserGroupProxy UserUserGroupProxy)
        {
            UserUserGroup newUserUserGroup = new UserUserGroup();
            newUserUserGroup.UserId = UserUserGroupProxy.UserId;
            newUserUserGroup.UserGroupId = UserUserGroupProxy.UserGroupId;
            DataContext.UserUserGroups.InsertOnSubmit(newUserUserGroup);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a UserUserGroup
        /// </summary>
        /// <param name="UserUserGroupProxy">UserUserGroup to Dlete</param>
        public override void Delete(UserUserGroupProxy UserUserGroupProxy)
        {            
            UserUserGroup userGroup = (from s in DataContext.UserUserGroups where s.UserGroupId == UserUserGroupProxy.UserGroupId && s.UserId == UserUserGroupProxy.UserId select s).Single();
            DataContext.UserUserGroups.DeleteOnSubmit(userGroup);
            DataContext.SubmitChanges();
        }
        #endregion
    }
}
