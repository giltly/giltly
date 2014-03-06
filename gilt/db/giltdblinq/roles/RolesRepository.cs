using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.roles
{
    /// <summary>
    /// Roles Repository
    /// </summary>
    public sealed class RolesRepository : GenericRepository<RolesProxy>, IRolesRepository
    {
        /// <summary>
        /// Initialize the Roles Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.Roles select new RolesProxy(u));
        }

        #region IRolesRepository
        /// <summary>
        /// Get all unassigned roles for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of available roles</returns>
        public IEnumerable<RolesProxy> GetUnassignedRoles(int UserId)
        {
            List<int> urp = (from ur in DataContext.UserRoles where ur.UserId == UserId select ur.RoleId).ToList();
            return this.GetAll().ToList().Where(x => !urp.Contains(x.Id));
        }
        /// <summary>
        /// Get all assigned roles for the UserId
        /// </summary>
        /// <param name="UserId">The User.Id</param>
        /// <returns>List of assigned roles</returns>
        public IEnumerable<RolesProxy> GetAssignedRoles(int UserId)
        {
            List<int> urp = (from ur in DataContext.UserRoles where ur.UserId == UserId select ur.RoleId).ToList();
            return this.GetAll().ToList().Where(x => urp.Contains(x.Id));
        }
        #endregion
    }
}
