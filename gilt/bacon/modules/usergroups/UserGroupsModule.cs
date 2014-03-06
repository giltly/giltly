using gilt.dblinq.proxy;
using gilt.dblinq.search;
using gilt.dblinq.user;
using Nancy.Security;

namespace gilt.bacon.modules.usergroups
{
    public partial class UsersGroupsModule : ModuleBase<IUserGroupsRepository, UserGroupsProxy>
    {
        IUserUserGroupsRepository _temp;

        public UsersGroupsModule(IUserGroupsRepository userGroupRepository, ISearchRepository searchRepository, IUserUserGroupsRepository userUserGroupRepository)
            : base(userGroupRepository, searchRepository)
        {
            _temp = userUserGroupRepository;

            this.RequiresAuthentication();
        }
    }
}
