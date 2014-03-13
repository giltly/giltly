using gilt.dblinq.proxy;
using System.Collections.Generic;

namespace gilt.dblinq.user
{
    /// <summary>
    /// UserGroups Repository
    /// </summary>
    public interface IUserGroupsRepository : IGenericRepository<UserGroupsProxy>
    {
        /// <summary>
        /// Get all unassigned UserGroups for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of available UserGroups</returns>
        IEnumerable<UserGroupsProxy> GetUnassignedUserGroups(int UserId);
        /// <summary>
        /// Get all assigned UserGroups for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of available UserGroups</returns>
        IEnumerable<UserGroupsProxy> GetAssignedUserGroups(int UserId);
    }
}
