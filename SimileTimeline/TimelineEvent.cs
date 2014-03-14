using Newtonsoft.Json;
using System;

namespace SimileTimeline
{    
    /// <summary>
    /// Hold information about a TimeLine Event
    /// Serialize with Newtonsoft.Json
    /// </summary>
    public sealed class TimelineEvent 
    {
        /// <summary>
        /// Default Gregorgain string format
        /// </summary>
        private const string DATETIME_DEFAULT = @"ddd dd MMM yyyy HH:mm:ss G\MT-0000";

        /// <summary>
        /// Start time in full date format
        /// </summary>
        [JsonIgnore]
        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                StartString = String.Format("{0:" + DATETIME_DEFAULT + "}", _start);                
            }
        }
        /// <summary>
        /// Start time in full date format
        /// </summary>
        [JsonProperty("start")]
        public string StartString { get; set; }
        private DateTime _start { get; set; }

        /// <summary>
        /// For imprecise beginnings. Start time in full date format
        /// </summary>
        [JsonIgnore]
        public DateTime LatestStart
        {
            get
            {
                return _latestStart;
            }
            set
            {
                _latestStart = value;
                LatestStartString = String.Format("{0:" + DATETIME_DEFAULT + "}", _latestStart);
            }
        }
        /// <summary>
        /// For imprecise beginnings. Start time in full date format
        /// </summary>
        [JsonProperty("latestStart")]
        public string LatestStartString { get; set; }
        private DateTime _latestStart { get; set; }
        /// <summary>
        /// For imprecise ends. Start time in full date format
        /// </summary>
        [JsonIgnore]
        public DateTime EarliestEnd
        {
            get
            {
                return _earliestEnd;
            }
            set
            {
                _earliestEnd = value;
                EarliestEndString = String.Format("{0:" + DATETIME_DEFAULT + "}", _earliestEnd);
            }
        }
        /// <summary>
        /// For imprecise ends. Start time in full date format
        /// </summary>
        [JsonProperty("earliestEnd")]
        public string EarliestEndString { get; set; }
        private DateTime _earliestEnd { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonIgnore]
        public DateTime End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                EndString = String.Format("{0:" + DATETIME_DEFAULT + "}", _end);
            }
        }
        [JsonProperty("end")]
        /// <summary>
        /// End time
        /// </summary>
        public string EndString { get; set; }
        private DateTime _end { get; set; }
        /// <summary>
        /// "true" or "false". Only applies to events with start/end times
        /// </summary>
        [JsonProperty("isDuration")]
        public bool IsDuration { get; set; }
        /// <summary>
        /// Text title that goes next to the tape in the Timeline. Also shown in the bubble
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Displayed inside the bubble with the event's title and image.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Url to an image that will be displayed in the bubble
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }
        /// <summary>
        /// The bubble's title text will be a hyper-link to this address.
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; set; }
        /// <summary>
        /// url. This image will appear next to the title text in the timeline if (no end date) or (durationEvent = false).
        /// If a start and end date are supplied, and durationEvent is true, the icon is not shown. If icon attribute is not set, a default icon from the theme is used.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        /// <summary>
        /// Color of the text and tape (duration events) to display in the timeline. 
        /// If the event has durationEvent = false, then the bar's opacity will be applied (default 20%).        
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }
        /// <summary>
        /// Color of the label text on the timeline. If not set, then the color attribute will be used.
        /// </summary>
        [JsonProperty("textColor")]
        public string TextColor { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TimelineEvent()
        {
            IsDuration = false;
            Title = null;
            Description = null;
            Image = null;
            Link = null;
            Icon = null;
            Color = null;
            TextColor = null;
        }

    }
}
