
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Geolocation Data Proxy
    /// </summary>
    public sealed class GeoDataProxy : ProxyBase
    {
        /// <summary>
        /// Source location
        /// </summary>
        public GeoLocationProxy GeoSource {get;set;}
        /// <summary>
        /// Destnation location
        /// </summary>
        public GeoLocationProxy GeoDestination { get; set; }

        /// <summary>
        /// Create a Geo Data from source and destination locations
        /// </summary>
        /// <param name="GeoSrc">Source location</param>
        /// <param name="GeoDst">Destination location</param>
        public GeoDataProxy(GeoLocationProxy GeoSrc, GeoLocationProxy GeoDst)  
        {
            GeoSource = GeoSrc;
            GeoDestination = GeoDst;
        }
    }
}
