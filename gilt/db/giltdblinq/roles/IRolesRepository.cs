using gilt.dblinq.proxy;
using System.Collections.Generic;

namespace gilt.dblinq.roles
{
    /// <summary>
    /// Roles Repository Interface
    /// </summary>
    public interface IRolesRepository : IGenericRepository<RolesProxy>
    {
        /// <summary>
        /// Get all unassigned Roles for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of available roles</returns>
        IEnumerable<RolesProxy> GetUnassignedRoles(int UserId);
        /// <summary>
        /// Get all assigned Roles for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of assigned roles</returns>
        IEnumerable<RolesProxy> GetAssignedRoles(int UserId);
    }
}
