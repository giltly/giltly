
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Events by Ip
    /// SQL view EventsByIp
    /// </summary>
    public sealed class EventsByIpProxy : ProxyBase
    {
        /// <summary>
        /// Ip address
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Number of events for Ip
        /// </summary>
        public int? EventCount { get; set; }        

        /// <summary>
        /// Create a eventsbyipproxy from a eventsbyip
        /// </summary>
        /// <param name="Eip">The eventsbyip</param>
        public EventsByIpProxy(EventsByIp Eip)
        {
            if (null != Eip)
            {
                Ip = Eip.Ip;
                EventCount = Eip.EventCount;
            }            
        }
    }
}
