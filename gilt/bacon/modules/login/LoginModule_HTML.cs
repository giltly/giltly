using gilt.dblinq.proxy;
using gilt.dblinq.user;
using Nancy.Authentication.Forms;

namespace gilt.bacon.modules.login
{
    public partial class LoginModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.LOGOUT] = x =>
            {
                return this.LogoutAndRedirect(GiltlyRoutes.INDEX);
            };
        }
    }
}
