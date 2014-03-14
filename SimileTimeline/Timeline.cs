using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SimileTimeline
{    
    /// <summary>
    /// Timeline class for showing snort events
    /// http://simile-widgets.org/ 
    /// </summary>
    public class Timeline 
    {
        /// <summary>
        /// Base url used to gin up url's for each event; by appending the wiki-section and the event's title; often a MediaWiki wiki URL
        /// </summary>
        private const string WIKI_URL = "http://simile.mit.edu";
        /// <summary>
        /// MediaWiki wiki section
        /// </summary>
        private const string WIKI_SECTION = "http://simile.mit.edu";
        /// <summary>
        /// Which parser should be used for dates/times. Values: "iso8601" or "Gregorian". Default is "Gregorian". Note: in JSON event source this property is dateTimeFormat.
        /// </summary>
        private const string DATETIME_DEFAULT = "Gregorian";
        /// <summary>
        /// List of event bandinfo for showing the time bands
        /// </summary>
        private List<BandInfo> EventBands;

        #region JSON Properties
        [JsonProperty("wiki-url")]
        public string WikiUrl {get;set;}
        [JsonProperty("wiki-section")]
        public string WikiSection { get; set; }
        [JsonProperty("date-time-format")]
        public string DateTimeFormat { get; set; }
        [JsonProperty("events")]
        public List<TimelineEvent> Events { get; set; }
        #endregion        

        /// <summary>
        /// Internal constructor for setting the default Timeline properties
        /// </summary>
        /// <param name="WikiUrl">Base url used to gin up url's for each event</param>
        /// <param name="WikiSection">MediaWiki wiki section</param>
        /// <param name="DateTimeFormat">Datetime format</param>
        /// <param name="Events">List of events</param>
        protected Timeline(
            string WikiUrl, 
            string WikiSection, 
            string DateTimeFormat, 
            List<TimelineEvent> Events)            
        {
            this.WikiUrl = WikiUrl;
            this.WikiSection = WikiSection;
            this.DateTimeFormat = DateTimeFormat;
            this.Events = Events;
        }

        /// <summary>
        /// Public ctor used to create a timeline from events
        /// </summary>
        /// <param name="Events"></param>
        /// <param name="Bands"></param>        
        public Timeline(
            List<TimelineEvent> Events,
            List<BandInfo> Bands)
            : this(WIKI_URL, WIKI_SECTION, DATETIME_DEFAULT, Events)
        {
            EventBands = Bands;
        }

        /// <summary>
        /// Get events as JSON object
        /// </summary>
        /// <returns>JSON event data object. The Events property contains the actual event data</returns>
        public string GetEventDataJson()
        {
            //Formatting.None is required so that var data ='' can get everything on one line
            return JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// Get a list of JSON strings representing the bandinformations
        /// </summary>
        /// <returns>List of JSON band info objects</returns>
        public List<string> GetBandInfoJson()
        {
            return EventBands.Select(x => x.GetJsonObject()).ToList();
        }
    }
}
