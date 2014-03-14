using Newtonsoft.Json;
using System;

namespace SimileTimeline
{
    /// <summary>
    /// Create a JSON represntation of a timeline band
    /// 
    /// {
    ///     eventSource: eventSource,
    ///     date: "Sat 12 Nov 2011 00:00:00 GMT-0000",
    ///     width: "70%",
    ///     layout: "original",
    ///     intervalUnit: Timeline.DateTime.DAY,
    ///     intervalPixels: 50
    /// }
    /// </summary>
    public class BandInfo
    {
        /// <summary>
        /// Default Gregorgain string format
        /// </summary>
        private const string DATETIME_DEFAULT = @"ddd dd MMM yyyy HH:mm:ss G\MT-0000";        
        /// <summary>
        /// The time to center the bandinfo
        /// </summary>
        [JsonIgnore]        
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                DateString = String.Format("{0:" + DATETIME_DEFAULT + "}", _date);
            }
        }
        [JsonProperty("date")]
        public string DateString { get; set; }
        private DateTime _date { get; set; }
        /// <summary>
        /// The width as percent of the band relative to the entire timeline
        /// </summary>
        [JsonProperty("width")]
        public string WidthPercent
        {
            get { return String.Format("{0}%", _width); }
        }
        private uint _width;
        /// <summary>
        /// Layout style
        /// </summary>
        [JsonProperty("layout")]
        public string Layout = "original";
        /// <summary>
        /// Interval for the band
        /// </summary>
        [JsonProperty("intervalUnit")]
        public DateTimeFormats IntervalUnit;
        /// <summary>
        /// Height of band in pixels
        /// </summary>
        [JsonProperty("intervalPixels")]
        public uint IntervalPixels;        
        /// <summary>
        /// Event source.  
        /// This is supposed to be a javascript object.
        /// It is returned as a string here and the consuming javascript attaches the source.
        /// </summary>
        [JsonProperty("eventSource")]
        public string eventSource = "eventSource";  

        /// <summary>
        /// Create a bandinfo object
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="WidthPercent"></param>
        /// <param name="Interval"></param>
        /// <param name="IntervalPix"></param>
        public BandInfo(            
            DateTime StartDate,
            uint WidthPercent,
            DateTimeFormats Interval,
            uint IntervalPix)
        {            
            Date = StartDate;
            _width = WidthPercent;
            IntervalUnit = Interval;
            IntervalPixels = IntervalPix;
        }

        /// <summary>
        /// Return a JSON object for the bandinfo
        /// </summary>
        /// <returns></returns>
        public string GetJsonObject()
        {
            //Formatting.None for sameline
            return JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }
    }
}
