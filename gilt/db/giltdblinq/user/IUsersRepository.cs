using System.Collections.Generic;
using gilt.dblinq.proxy;

namespace gilt.dblinq.user
{
    /// <summary>
    /// Users Repository
    /// </summary>
    public interface IUsersRepository : IGenericRepository<UsersProxy>
    {
        /// <summary>
        /// Get a user by email address
        /// </summary>
        /// <param name="Email">Email Address</param>
        /// <returns>UserProxy</returns>
        UsersProxy GetUserByEmail(string Email);
        /// <summary>
        /// Update a User
        /// </summary>
        /// <param name="U">User to Update</param>
        new void Update(UsersProxy U);
    }
}
