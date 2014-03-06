using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.bacon.auth;
using Nancy;
using Nancy.Security;
using Effortless.Net.Encryption;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.auth
{
    public class TestUserDatabase : IDatabaseUserMapping
    {
        public static string USERNAME = "admin@giltly.com";
        public static string PASSWORD = "password";
        public static string FIRSTNAME = "Testing";
        public static string LASTNAME = "User";
        public static Guid USERGUID = new Guid("{4A1E6B8A-9862-485C-9677-AC69B67DB496}");

        private List<Tuple<string, string, Guid, UsersProxy>> _users = new List<Tuple<string, string, Guid, UsersProxy>>();

        public TestUserDatabase()
        {
            User u = new User();
            u.CreatedOn = DateTime.Now;
            u.Email = USERNAME;
            u.FirstName = FIRSTNAME;
            u.LastName = LASTNAME;
            u.Guid = USERGUID;
            _users.Add(new Tuple<string, string, Guid, UsersProxy>(USERNAME, PASSWORD, USERGUID, new UsersProxy(u)));
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var userRecord = _users.Where(u => u.Item3 == identifier).FirstOrDefault();

            return userRecord == null
                       ? null
                       : new GiltyUserIdentity { UserName = userRecord.Item1, UserProxy = userRecord.Item4 };
        }

        public Guid? ValidateUser(string username, string password)
        {
            var userRecord = _users.Where(u => (u.Item1 == username)
                && (u.Item2 == password)).FirstOrDefault();

            if (userRecord == null)
            {
                return null;
            }

            return userRecord.Item3;
        }

        public string HashUser(string email, string password)
        {
            return Hash.Create(HashType.SHA512, String.Format("{0}{1}", email, password), ConfigurationManager.AppSettings["Key"], false);
        }
    }
}
