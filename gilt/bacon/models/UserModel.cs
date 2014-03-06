using gilt.bacon.auth;
using gilt.dblinq.proxy;
using System.Collections.Generic;

namespace gilt.bacon.models
{
    public class UserModel
    {        
        public GiltyUserIdentity GiltlyIdentity { get; private set; }
        public UsersProxy UserProxy { get; private set; }
        public List<bool> IsLoggedIn { get; private set; }
        public List<bool> IsAdminUser { get; private set; }

        public UserModel(GiltyUserIdentity giltIdent)
        {
            GiltlyIdentity = giltIdent;            
            IsLoggedIn = null;
            IsAdminUser = null; 

            if (null != giltIdent)
            {
                UserProxy = GiltlyIdentity.UserProxy;
                SetIsLoggedIn();
                SetIsAdminUser();
            }      
        }

        private void SetIsLoggedIn()
        {
            if (!string.IsNullOrEmpty(UserProxy.Email))
            {
                IsLoggedIn = new List<bool>();
                IsLoggedIn.Add(true);
            }
        }

        private void SetIsAdminUser()
        {
            if (!string.IsNullOrEmpty(UserProxy.Email))
            {
                IsAdminUser = new List<bool>();
                IsAdminUser.Add(true);
            }
        }
    }
}