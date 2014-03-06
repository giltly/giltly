using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.user
{
    /// <summary>
    /// User Repository
    /// </summary>
    public sealed class UserRepository : GenericRepository<UsersProxy>, IUsersRepository
    {
        /// <summary>
        /// Initialize the User Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.Users select new UsersProxy (u));
        }

        #region IUsersRepository
        /// <summary>
        /// Get a user by email address
        /// </summary>
        /// <param name="Email">Email Address</param>
        /// <returns>UserProxy</returns>
        public UsersProxy GetUserByEmail(string Email)
        {
            return _genericQuery.Where(u => u.Email == Email).FirstOrDefault();
        }
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userProxy"></param>
        public override void Add(UsersProxy userProxy)
        {
            User createUser = new User();            
            createUser.Guid = userProxy.Guid;
            createUser.CreatedOn = userProxy.CreatedOn;
            createUser.Password = userProxy.Password;
            createUser.Email = userProxy.Email;
            createUser.FirstName = userProxy.FirstName;
            createUser.LastName = userProxy.LastName;
            createUser.ActiveSearch = userProxy.ActiveSearch;
            DataContext.Users.InsertOnSubmit(createUser);            
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// ------------------------------
        /// Allow the User to Update only the following columns
        /// ------------------------------
        /// Email
        /// FirstName
        /// LastName
        /// ActiveSearch
        /// Password
        /// </summary>
        /// <param name="userProxy"></param>
        public override void Update(UsersProxy userProxy)
        {
            User loggedInUser = (from u in DataContext.Users where u.Guid == userProxy.Guid select u).Single();
            loggedInUser.Email = userProxy.Email;
            loggedInUser.FirstName = userProxy.FirstName;
            loggedInUser.LastName = userProxy.LastName;
            loggedInUser.ActiveSearch = userProxy.ActiveSearch;
            loggedInUser.Password = userProxy.Password;            
            DataContext.SubmitChanges();
        }
        #endregion
    }
}
