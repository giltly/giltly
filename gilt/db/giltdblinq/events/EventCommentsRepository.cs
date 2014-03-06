using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.events
{
    /// <summary>
    /// Event Comments Repository
    /// </summary>
    public class EventCommentsRepository : GenericRepository<EventCommentsProxy>, IEventCommentsRepository
    {
        /// <summary>
        /// Initialize Event Comments Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from e in DataContext.EventComments select new EventCommentsProxy(e));                             
        }

        #region IEventCommentsRepository CRUD
        /// <summary>
        /// Add an EventComment
        /// </summary>
        /// <param name="EventProxy">Event to Add</param>
        public override void Add(EventCommentsProxy EventCommentProxy)
        {
            EventComment newEventComment = new EventComment();
            newEventComment.EventId = EventCommentProxy.EventId;
            newEventComment.SensorId = EventCommentProxy.SensorId;
            newEventComment.Comment = EventCommentProxy.Comment;
            newEventComment.CreatedBy = EventCommentProxy.CreatedBy;
            newEventComment.CreatedOn = EventCommentProxy.CreatedOn;
            DataContext.EventComments.InsertOnSubmit(newEventComment);
            DataContext.SubmitChanges();
        }
        #endregion
    }
}