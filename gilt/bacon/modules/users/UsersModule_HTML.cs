using gilt.dblinq.proxy;
using gilt.dblinq.user;

namespace gilt.bacon.modules.users
{
    public partial class UsersModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.USER_PROFILE] = parameters =>
            {
                return this.RenderView("UserProfile");
            };
            Get[GiltlyRoutes.USER_CHANGEPASSWORD] = parameters =>
            {
                return this.RenderView("ChangePassword");
            };
            Get[GiltlyRoutes.USER_LIST] = parameters =>
            {
                return this.RenderView("UserList");
            };
            Get[GiltlyRoutes.USER_ADD] = parameters =>
            {
                return this.RenderView("UserAdd");
            };
        }
    }
}
