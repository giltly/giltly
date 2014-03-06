using gilt.dblinq.proxy;
using gilt.dblinq.search;
using gilt.dblinq.user;
using System.Globalization;

namespace gilt.bacon.modules
{
    /// <summary>
    /// Index Module
    /// </summary>
    public sealed class IndexModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        /// <summary>
        /// Index Module
        /// </summary>
        /// <param name="UserRepository">User Repository</param>
        /// <param name="SearchRepository">Search Repository</param>
        public IndexModule(IUsersRepository UserRepository, ISearchRepository SearchRepository) : base(UserRepository, SearchRepository)
        {
            Get[GiltlyRoutes.INDEX] = parameters =>
            {
                this.Context.Culture = new CultureInfo("en-US");
                return this.RenderView("Index");
            };
        }
    }
}
