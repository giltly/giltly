using System;
using System.Collections.Generic;

namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Event Proxy
    /// </summary>
    public sealed class EventProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Snort Event Id
        /// </summary>
        public decimal EventId { get; set; }
        /// <summary>
        /// Snort Sensor Id
        /// </summary>
        public decimal SensorId { get; set; }
        /// <summary>
        /// Snort Signature Id
        /// </summary>
        public decimal SignatureId { get; set; }
        /// <summary>
        /// Timestamp of the event
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Timestamp as a short string
        /// </summary>
        public string TimeStampShortString { get; set; }
        /// <summary>
        /// Timestamp as a long string
        /// </summary>
        public string TimeStampLongString { get; set; }

        /// <summary>
        /// List of all the event comments for this event
        /// </summary>
        public List<EventCommentsProxy> EventComments { get; set; }
        /// <summary>
        /// Ipheader data for this event
        /// </summary>
        public IpHeaderProxy IpHeader { get; set; }
        /// <summary>
        /// Source geolocation for this event
        /// </summary>
        public GeoLocationProxy GeoLocationSource { get; set; }
        /// <summary>
        /// The destination geolocation for this event
        /// </summary>
        public GeoLocationProxy GeoLocationDestination { get; set; }
        /// <summary>
        /// All the snort data for this event
        /// </summary>
        public List<DataProxy> Data { get; set; }
        /// <summary>
        /// Icmp header for this event
        /// </summary>
        public IcmpHeaderProxy IcmpHeader { get; set; }
        /// <summary>
        /// Tcp header for this event
        /// </summary>
        public TcpHeaderProxy TcpHeader { get; set; }
        /// <summary>
        /// Udp header for this event
        /// </summary>
        public UdpHeaderProxy UdpHeader { get; set; }
        /// <summary>
        /// Flag data for this event
        /// </summary>
        public FlagProxy Flag { get; set; }
        /// <summary>
        /// Signature of this event
        /// </summary>
        public SignatureProxy Signature { get; set; }
        /// <summary>
        /// Signature classificaiton of this event
        /// </summary>
        public SignatureClassificationProxy SignatureClassification { get; set; }
        /// <summary>
        /// Source Country code for this event
        /// </summary>
        public CountryCodeProxy CountryCodeSource { get; set; }
        /// <summary>
        /// Destination Country code for this event
        /// </summary>
        public CountryCodeProxy CountryCodeDestination { get; set; }

        /// <summary>
        /// Create a event proxy from a event
        /// </summary>
        /// <param name="E">The event</param>
        public EventProxy(Event E) 
        {
            if (null != E)
            {
                Id = E.Id;
                EventId = E.EventId;
                SensorId = E.SensorId;
                SignatureId = E.SignatureId;
                TimeStamp = E.TimeStamp;
                TimeStampShortString = E.TimeStamp.ToString("u");
                TimeStampLongString = E.TimeStamp.ToString("u"); 
            }
        }
    }
}
