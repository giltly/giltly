using Nancy.Authentication.Forms;
using System;

namespace gilt.bacon.auth
{
    /// <summary>
    /// DatabaseUser Mapping
    /// </summary>
    public interface IDatabaseUserMapping : IUserMapper
    {
        /// <summary>
        /// Validate a user with the database table User
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Guid? ValidateUser(string Email, string Password);
        /// <summary>
        /// Create a password hash for the user
        /// </summary>
        /// <param name="Email">Email address</param>
        /// <param name="Password">Password</param>
        /// <returns>The password hash</returns>
        string HashUser(string Email, string Password);
    }
}
