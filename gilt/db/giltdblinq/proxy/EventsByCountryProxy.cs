
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Event by country proxy.
    /// SQL View EventsByCountry
    /// </summary>
    public sealed class EventsByCountryProxy : ProxyBase
    {
        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// Three character country code
        /// </summary>
        public string CountryCode3 { get; set; }
        /// <summary>
        /// Number of events in country
        /// </summary>
        public int? CountryCount { get; set; }

        /// <summary>
        /// Create a eventbycountryproxy from a eventsbycountry view query
        /// </summary>
        /// <param name="Ebc">The eventsbycountry</param>
        public EventsByCountryProxy(EventsByCountry Ebc)
        {
            if (null != Ebc)
            {
                CountryCode = Ebc.CountryCode;
                CountryCode3 = Ebc.CountryCode3;
                CountryCount = Ebc.CountryCount;
            }
        }

    }
}
