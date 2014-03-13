using gilt.dblinq.proxy;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.user
{
    /// <summary>
    /// UserGroups Repository
    /// </summary>
    public sealed class UserGroupsRepository : GenericRepository<UserGroupsProxy>, IUserGroupsRepository
    {
        /// <summary>
        /// Initialize the UserGroups Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.UserGroups select new UserGroupsProxy(u));
        }

        #region IUserGroupsRepository
        /// <summary>
        /// Get all unassigned UserGroups for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of available UserGroups</returns>
        public IEnumerable<UserGroupsProxy> GetUnassignedUserGroups(int UserId)
        {
            List<int> urp = (from ur in DataContext.UserUserGroups where ur.UserId == UserId select ur.UserGroupId).ToList();
            return this.GetAll().ToList().Where(x => !urp.Contains(x.Id));
        }
        /// <summary>
        /// Get all assigned UserGroups for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of available UserGroups</returns>
        public IEnumerable<UserGroupsProxy> GetAssignedUserGroups(int UserId)
        {
            List<int> urp = (from ur in DataContext.UserUserGroups where ur.UserId == UserId select ur.UserGroupId).ToList();
            return this.GetAll().ToList().Where(x => urp.Contains(x.Id));
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Update a UserGroup
        /// </summary>
        /// <param name="UserGroupProxy">UserGroup to Update</param>
        public override void Update(UserGroupsProxy UserGroupProxy)
        {
            UserGroup existingUserGroup = (from s in DataContext.UserGroups where s.Name == UserGroupProxy.Name select s).Single();
            existingUserGroup.Name = UserGroupProxy.Name;
            existingUserGroup.Description = UserGroupProxy.Description;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a UserGroup
        /// </summary>
        /// <param name="UserGroupProxy">UserGroup to Add</param>
        public override void Add(UserGroupsProxy UserGroupProxy)
        {
            UserGroup newUserGroup = new UserGroup();
            newUserGroup.Name = UserGroupProxy.Name;
            newUserGroup.Description = UserGroupProxy.Description;
            DataContext.UserGroups.InsertOnSubmit(newUserGroup);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a UserGroup
        /// </summary>
        /// <param name="UserGroupProxy">UserGroup to delete</param>
        public override void Delete(UserGroupsProxy UserGroupProxy)
        {            
            UserGroup userGroup = (from s in DataContext.UserGroups where s.Name == UserGroupProxy.Name select s).Single();
            DataContext.UserGroups.DeleteOnSubmit(userGroup);
            DataContext.SubmitChanges();
        }
        #endregion
    }
}
