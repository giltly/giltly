using System;

namespace gilt.dblinq.proxy
{
    /// <summary>
    /// EventComment Proxy
    /// </summary>
    public sealed class EventCommentsProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The snort event id
        /// </summary>
        public decimal EventId { get; set; }
        /// <summary>
        /// The snort sensor id
        /// </summary>
        public decimal SensorId { get; set; }
        /// <summary>
        /// The comment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// The user id of the creating user
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The datetime created
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The datetime created as a string
        /// </summary>
        public string CreatedOnString { get; set; }

        /// <summary>
        /// Nancy Bindable Constructor
        /// </summary>        
        public EventCommentsProxy()
            : base()
        {
        }

        /// <summary>
        /// Create an event comment proxy from an event comment
        /// </summary>
        /// <param name="E">The event comment</param>
        public EventCommentsProxy(EventComment E)
            : base()
        {
            if (null != E)
            {
                Id = E.Id;
                EventId = E.EventId;
                SensorId = E.SensorId;
                Comment = E.Comment;
                CreatedBy = E.CreatedBy;
                CreatedOn = E.CreatedOn;
                CreatedOnString = CreatedOn.ToString("u");
            }
        }
    }
}
