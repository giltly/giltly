using gilt.dblinq.events;
using gilt.dblinq.proxy;
using gilt.dblinq.search;

namespace gilt.bacon.modules.login
{
    /// <summary>
    /// Admin Module
    /// </summary>
    public partial class AdminModule : ModuleBase<IEventRepository, EventProxy>
    {
        /// <summary>
        /// Admin Module
        /// </summary>
        /// <param name="EventRepository">Event Repository</param>
        /// <param name="SearchRepository">Search Repository</param>
        public AdminModule(IEventRepository EventRepository, ISearchRepository SearchRepository)
            : base(EventRepository, SearchRepository)
        {
        }
    }
}
