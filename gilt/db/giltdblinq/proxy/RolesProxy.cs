
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Roles Proxy
    /// </summary>
    public sealed class RolesProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Create a RolesProxy from a Role
        /// </summary>
        /// <param name="R">The Role</param>
        public RolesProxy(Role R)            
        {
            if (null != R)
            {
                Id = R.Id;
                Name = R.Name;
                Description = R.Description;
            }
        }
    }
}
