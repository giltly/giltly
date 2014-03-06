using gilt.dblinq.proxy;
using gilt.dblinq.roles;
using gilt.dblinq.search;
using Nancy.Security;

namespace gilt.bacon.modules.roles
{
    /// <summary>
    /// Role Module
    /// </summary>
    public partial class RolesModule : ModuleBase<IRolesRepository, RolesProxy>
    {
        private IUserRolesRepository _userRolesRepository;

        /// <summary>
        /// Create a role module
        /// </summary>
        /// <param name="RolesRepository">Role Repository</param>
        /// <param name="SearchRepository">Search Repository</param>
        /// <param name="UserRolesRepository">UserRole Repository</param>
        public RolesModule(IRolesRepository RolesRepository, ISearchRepository SearchRepository, IUserRolesRepository UserRolesRepository)             
            : base(RolesRepository,SearchRepository)
        {
            _userRolesRepository = UserRolesRepository;
            this.RequiresAuthentication();
        }
    }
}
