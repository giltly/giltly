using gilt.bacon.auth;
using gilt.dblinq.proxy;
using gilt.dblinq.search;
using gilt.dblinq.user;

namespace gilt.bacon.modules.login
{
    public partial class LoginModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        private IDatabaseUserMapping _userMapping;

        public LoginModule(IUsersRepository userRepository, ISearchRepository searchRepository, IDatabaseUserMapping UserMapping)
            : base(userRepository, searchRepository)
        {
            _userMapping = UserMapping;
        }
    }
}
