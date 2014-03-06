using gilt.bacon.auth;
using gilt.dblinq.proxy;
using gilt.dblinq.search;
using gilt.dblinq.user;
using Nancy.Security;

namespace gilt.bacon.modules.users
{
    public partial class UsersModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        private IDatabaseUserMapping _userMapping;

        public UsersModule(IUsersRepository userRepository, ISearchRepository searchRepository, IDatabaseUserMapping UserMapping)
            : base(userRepository, searchRepository)
        {
            this.RequiresAuthentication();

            _userMapping = UserMapping;
        }
    }
}
