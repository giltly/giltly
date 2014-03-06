using System;

namespace gilt.dblinq.proxy
{
    /// <summary>
    /// A User Proxy
    /// </summary>
    public sealed class UsersProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// UID for the user
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Date the user was created on
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// Hashed password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The user's active search
        /// </summary>
        public int? ActiveSearch { get; set; }
        /// <summary>
        /// Password1 - used for change password
        /// </summary>
        public string Password1 { get; set; }
        /// <summary>
        /// Password2 - used for change password
        /// </summary>
        public string Password2 { get; set; }

        /// <summary>
        /// Default Constructor for Nancy Model .Bind()
        /// </summary>
        public UsersProxy()
        {
        }

        /// <summary>
        /// Create a user proxy from a user
        /// </summary>
        /// <param name="U">The User</param>
        public UsersProxy(User U) 
        {
            if (null != U)
            {
                Id = U.Id;
                Guid = U.Guid;
                CreatedOn = U.CreatedOn;
                Password = U.Password;
                Email = U.Email;
                FirstName = U.FirstName;
                LastName= U.LastName;
                ActiveSearch = U.ActiveSearch;
            }
        }
    }
}
