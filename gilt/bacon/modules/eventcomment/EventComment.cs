using gilt.dblinq.events;
using gilt.dblinq.proxy;
using gilt.dblinq.search;
using Nancy.Security;

namespace gilt.bacon.modules.events
{
    /// <summary>
    /// Event Comment Module
    /// </summary>
    public partial class EventCommentModule : ModuleBase<IEventCommentsRepository, EventCommentsProxy>
    {
        private IEventRepository _eventRepository = null;

        /// <summary>
        /// Event Comments Moudle
        /// </summary>
        /// <param name="EventCommentRepository">Event Comment Repository</param>
        /// <param name="SearchRepository">Search Repository</param>
        public EventCommentModule(IEventCommentsRepository EventCommentRepository, ISearchRepository SearchRepository, IEventRepository EventRepository)
            : base(EventCommentRepository, SearchRepository)
        {
            this.RequiresAuthentication();
            _eventRepository = EventRepository;
        }
    }
}
