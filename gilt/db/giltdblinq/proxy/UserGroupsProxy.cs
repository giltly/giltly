
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// UserGroups Proxy
    /// </summary>
    public sealed class UserGroupsProxy : ProxyBase
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
        /// Default Constructor for Nancy Model .Bind()
        /// </summary>
        public UserGroupsProxy()
        {
        }

        /// <summary>
        /// Create a UserGroupsProxy from a UserGroup
        /// </summary>
        /// <param name="G">The UserGroup</param>
        public UserGroupsProxy(UserGroup G)
        {
            if (null != G)
            {
                Id = G.Id;
                Name = G.Name;
                Description = G.Description;
            }
        }
    }
}
