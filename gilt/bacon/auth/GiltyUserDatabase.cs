using Effortless.Net.Encryption;
using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.user;
using gilt.util;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace gilt.bacon.auth
{
    /// <summary>
    /// Defines the link between the user table and the nancy IDatabaseUserMapping
    /// </summary>
    public class GiltyUserDatabase : IDatabaseUserMapping
    {
        private IUsersRepository _userRepository = null;
        private List<Tuple<string, string, Guid, UsersProxy>> _users = new List<Tuple<string, string, Guid, UsersProxy>>();

        /// <summary>
        /// Create a new user databaes
        /// </summary>
        public GiltyUserDatabase()
        {
            _userRepository = new UserRepository();
            _users.AddRange((from u in _userRepository.GetAll() select new Tuple<string, string, Guid, UsersProxy>(u.Email, u.Password, u.Guid,u)).ToList());
        }

        /// <summary>
        /// Get the logged in user from the Guid identifier.  From Nancy IUserMapper
        /// </summary>
        /// <param name="Identifier"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public IUserIdentity GetUserFromIdentifier(Guid Identifier, NancyContext Context)
        {
            var userRecord = _users.Where(u => u.Item3 == Identifier).FirstOrDefault();

            return userRecord == null
                       ? null
                       : new GiltyUserIdentity { UserName = userRecord.Item1, UserProxy = userRecord.Item4 };
        }

        /// <summary>
        /// Validate the a user exists in the database by email and that the password matches
        /// </summary>
        /// <param name="Email">The user's email</param>
        /// <param name="Password">The user's password</param>
        /// <returns></returns>
        public Guid? ValidateUser(string Email, string Password)
        {
            var userRecord = _users.Where(u => (u.Item1 == Email)
                && (u.Item2 == HashUser(u.Item1, Password))).FirstOrDefault();

            if (userRecord == null)
            {
                return null;
            }

            return userRecord.Item3;
        }

        /// <summary>
        /// Create a SHA512 hash from the password and email; which will be unique
        /// </summary>
        /// <param name="Email">Email address of user</param>
        /// <param name="Password">Password for user</param>
        /// <returns></returns>
        public string HashUser(string Email, string Password)
        {
            //
            // don't want to encrypt the data
            // store the 1 way hash of the email [unique] and the password
            //
            return Hash.Create(HashType.SHA512, String.Format("{0}{1}", Email, Password), ConfigurationManager.AppSettings["Key"], false);
        }
    }
}