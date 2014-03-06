using gilt.dblinq.proxy;
using Nancy.Security;
using System.Collections.Generic;

namespace gilt.bacon.auth
{
    /// <summary>
    /// Giltly User Identity
    /// </summary>
    public class GiltyUserIdentity : IUserIdentity
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Claims
        /// </summary>
        public IEnumerable<string> Claims { get; set; }
        /// <summary>
        /// UserProxy object
        /// </summary>
        public UsersProxy UserProxy { get; set; }
    }
}