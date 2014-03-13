using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.roles
{
    /// <summary>
    /// UserRoles Repository
    /// </summary>
    public sealed class UserRolesRepository : GenericRepository<UserRolesProxy>, IUserRolesRepository
    {
        /// <summary>
        /// Initialize the UserRoles Query
        /// </summary>        
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.UserRoles select new UserRolesProxy(u));
        }

        #region IUserRolesRepository CRUD
        /// <summary>
        /// Update a UserRole
        /// </summary>
        /// <param name="UserRoleProxy">UserRole to Update</param>
        public override void Update(UserRolesProxy UserRoleProxy)
        {
            UserRole existingUserRole = (from s in DataContext.UserRoles where s.UserId == UserRoleProxy.UserId && s.RoleId == UserRoleProxy.RoleId select s).Single();
            existingUserRole.UserId = UserRoleProxy.UserId;
            existingUserRole.RoleId = UserRoleProxy.RoleId;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a UserRole
        /// </summary>
        /// <param name="UserRoleProxy">UserRole to Add</param>
        public override void Add(UserRolesProxy UserRoleProxy)
        {
            UserRole newUserRole = new UserRole();
            newUserRole.UserId = UserRoleProxy.UserId;
            newUserRole.RoleId = UserRoleProxy.RoleId;
            DataContext.UserRoles.InsertOnSubmit(newUserRole);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a UserRole
        /// </summary>
        /// <param name="UserRoleProxy">UserRole to Delete</param>
        public override void Delete(UserRolesProxy UserRoleProxy)
        {
            UserRole userRole = (from s in DataContext.UserRoles where s.UserId == UserRoleProxy.UserId && s.RoleId == UserRoleProxy.RoleId select s).Single();
            DataContext.UserRoles.DeleteOnSubmit(userRole);
            DataContext.SubmitChanges();
        }
        #endregion

    }
}
