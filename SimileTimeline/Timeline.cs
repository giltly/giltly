using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SimileTimeline
{    
    public class Timeline 
    {
        private const string WIKI_URL = "http://simile.mit.edu";
        private const string WIKI_SECTION = "http://simile.mit.edu";
        private const string DATETIME_DEFAULT = "Gregorian";

        [JsonProperty("wiki-url")]
        public string WikiUrl {get;set;}
        [JsonProperty("wiki-section")]
        public string WikiSection { get; set; }
        [JsonProperty("date-time-format")]
        public string DateTimeFormat { get; set; }
        [JsonProperty("events")]
        public List<TimelineEvent> Events { get; set; }

        protected Timeline()
        {
            WikiSection = WIKI_URL;
            WikiUrl = WIKI_SECTION;
            DateTimeFormat = DATETIME_DEFAULT;
            Events = new List<TimelineEvent>();
        }

        protected Timeline(string WikiUrl, string WikiSection, List<TimelineEvent> Events)
            : this(WikiUrl, WikiSection, DATETIME_DEFAULT, Events)
        {
        }

        protected Timeline(string WikiUrl, string WikiSection, string DateTimeFormat, List<TimelineEvent> Events)            
        {
            this.WikiUrl = WikiUrl;
            this.WikiSection = WikiSection;
            this.DateTimeFormat = DateTimeFormat;
            this.Events = Events;
        }

        /// <summary>
        /// The main constructor.  
        /// Called from a Nancy Module and returning javascript for eval
        /// </summary>
        /// <param name="Events"></param>
        public Timeline(List<TimelineEvent> Events)
            : this(WIKI_URL, WIKI_SECTION, DATETIME_DEFAULT, Events)
        {
        }

        private string GetJavaScript()
        {
            return @"
                
                $(document).ready(function() {
                    var data = '" + this.EventsDataToJson() + @"';
                    $('#EventTimeLine').replaceWith($('<div id=""my-timeline"" class=""timelineDiv""></div>'));
                    var eventSource = new Timeline.DefaultEventSource();
                    var bandInfos = [
                      Timeline.createBandInfo({
                          eventSource: eventSource,
                          date: ""Sat 12 Nov 2011 00:00:00 GMT-0000"",
                          width: ""20%"",  
                          layout: ""original"",
                          intervalUnit: Timeline.DateTime.MINUTE,
                          intervalPixels: 50
                      }),

                      Timeline.createBandInfo({
                          eventSource: eventSource,
                          date: ""Sat 12 Nov 2011 00:00:00 GMT-0000"",
                          width: ""30%"",  
                          layout: ""original"",
                          intervalUnit: Timeline.DateTime.HOUR,
                          intervalPixels: 200
                      }),

                      Timeline.createBandInfo({
                        eventSource: eventSource,            
                        width: ""50%"",                    
                        intervalUnit: Timeline.DateTime.DAY,
                        intervalPixels: 400
                    })
                    ];
                    bandInfos[0].highlight = true;
                    bandInfos[1].syncWith = 0;
                    bandInfos[1].highlight = true;
                    bandInfos[2].syncWith = 0;
                    bandInfos[2].highlight = true;

                    tl = Timeline.create(document.getElementById(""my-timeline""), bandInfos, Timeline.HORIZONTAL);
                    eventSource.loadJSON(JSON.parse(data), '.');
                });
            ";
        }

        public string GetTimeline()
        {
            return this.GetJavaScript();            
        }

        private string EventsDataToJson()
        {
            //Formatting.None is required so that var data ='' can get everything on one line
            return JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
