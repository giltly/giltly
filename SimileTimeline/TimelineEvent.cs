using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SimileTimeline
{    
    /// <summary>
    /// Hold information about a TimeLine Event
    /// Serialize with Newtonsoft.Json
    /// </summary>
    public sealed class TimelineEvent 
    {
        private const string DATETIME_DEFAULT = @"ddd dd MMM yyyy HH:mm:ss G\MT-0000";

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
        [JsonProperty("start")]
        public string StartString { get; set; }
        private DateTime _start { get; set; }

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
        [JsonProperty("latestStart")]
        public string LatestStartString { get; set; }
        private DateTime _latestStart { get; set; }

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
        [JsonProperty("earliestEnd")]
        public string EarliestEndString { get; set; }
        private DateTime _earliestEnd { get; set; }

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
        public string EndString { get; set; }
        private DateTime _end { get; set; }

        [JsonProperty("isDuration")]
        public bool IsDuration { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("textColor")]
        public string TextColor { get; set; }

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
