using gilt.dblinq.proxy;
using gilt.dblinq.user;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;

namespace gilt.bacon.modules.login
{
    public partial class LoginModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        protected override void PostResponses()
        {
            Post[GiltlyRoutes.LOGIN] = x =>
            {
                var userGuid = _userMapping.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);
                if (userGuid == null)
                {
                    return this.Context.GetRedirect(String.Format("{0}?error=true", GiltlyRoutes.INDEX));
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };
        }
    }
}
