using gilt.dblinq;
using gilt.dblinq.events;
using gilt.dblinq.proxy;
using gilt.dblinq.search;
using System;
using System.Linq;
using Nancy.Security;
using Nancy;
using Nancy.Responses;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace gilt.bacon.modules.events
{
    /// <summary>
    /// Event Comment Module
    /// </summary>
    public partial class EventCommentModule : ModuleBase<IEventCommentsRepository, EventCommentsProxy>
    {
        /// <summary>
        /// Admin POST Handlers
        /// </summary>
        protected override void PostResponses()
        {
            Post[GiltlyRoutes.EVENTCOMMENT_CREATE] = x =>
            {
                //input name=Comment comes over from the form
                EventCommentsProxy requestedEventComment = this.Bind<EventCommentsProxy>();
                //Id is the Event.Id from the hidden form element not the PK from event comments
                Int32 id = requestedEventComment.Id;                
                //get the exisiting event
                EventProxy existingEvent = _eventRepository.FindBy(y => y.Id == id).SingleOrDefault();
                requestedEventComment.SensorId = existingEvent.SensorId;
                requestedEventComment.EventId = existingEvent.EventId;
                requestedEventComment.CreatedBy = CurrentLoggedInUserId;
                requestedEventComment.CreatedOn = DateTime.Now;
                return _crudDelegate(requestedEventComment, CrudEnum.Add, GiltlyRoutes.INDEX);
            };
        }
    }
}
