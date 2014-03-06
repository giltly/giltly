
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// UserRoles Proxy
    /// </summary>
    public sealed class UserRolesProxy : ProxyBase
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Role Id
        /// </summary>
        public int RoleId { get; set; }        

        /// <summary>
        /// Create a UserRolesProxy from a UserRole
        /// </summary>
        /// <param name="R">The UserRole</param>
        public UserRolesProxy(UserRole R)        
        {
            if (null != R)
            {
                UserId = R.UserId;
                RoleId= R.RoleId;                
            }
        }
    }
}
