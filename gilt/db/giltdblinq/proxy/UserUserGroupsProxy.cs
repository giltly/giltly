
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// A UserUserGroup Proxy
    /// </summary>
    public sealed class UserUserGroupProxy
    {
        /// <summary>
        /// The user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The user group id
        /// </summary>
        public int UserGroupId { get; set; }

        /// <summary>
        /// Create a userusergroup proxy from a user user group
        /// </summary>
        /// <param name="Uug">The UserUserGroup</param>
        public UserUserGroupProxy(UserUserGroup Uug)
            : base()
        {
            if (null != Uug)
            {
                UserId = Uug.UserId;
                UserGroupId = Uug.UserGroupId;
            }
        }
    }
}
