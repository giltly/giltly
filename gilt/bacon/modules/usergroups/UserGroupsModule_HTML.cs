using gilt.dblinq.proxy;
using gilt.dblinq.user;

namespace gilt.bacon.modules.usergroups
{
    public partial class UsersGroupsModule : ModuleBase<IUserGroupsRepository, UserGroupsProxy>
    {
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.USERGROUP_LIST] = parameters =>
            {
                return this.RenderView("UserGroupList");
            };
            Get[GiltlyRoutes.USERGROUP_ADD] = parameters =>
            {
                return this.RenderView("UserGroupAdd");
            };
            Get[GiltlyRoutes.USERGROUP_ASSIGNMENT] = parameters =>
            {
                return this.RenderView("UserGroupAssignment");
            }; 
        } 
    }
}
