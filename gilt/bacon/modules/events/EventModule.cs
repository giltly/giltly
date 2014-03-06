using gilt.dblinq.events;
using gilt.dblinq.proxy;
using gilt.dblinq.search;
using Nancy.Security;

namespace gilt.bacon.modules.events
{
    /// <summary>
    /// Event Repository
    /// </summary>
    public partial class EventModule : ModuleBase<IEventRepository, EventProxy>
    {
        private SearchProxy _activeSearch = null;

        /// <summary>
        /// Event Module
        /// </summary>
        /// <param name="EventRepository">Event Repository</param>
        /// <param name="SearchRepository">Searcg Repository</param>
        public EventModule(IEventRepository EventRepository, ISearchRepository SearchRepository) 
            : base (EventRepository, SearchRepository)
        {
            this.RequiresAuthentication();
        }
    }
}
