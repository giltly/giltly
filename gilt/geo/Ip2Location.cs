using gilt.dblinq;
using gilt.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Runtime.Caching;

namespace gilt.geo
{
    /// <summary>
    /// Convert IpAdress to Locations using the MaxMind Lite Database
    /// </summary>
    public sealed class Ip2Location
    {
        private static List<Tuple<GeoIp, GeoLocation, long, long>> _ipAndLocations;
        private static ObjectCache _cache = MemoryCache.Default;  
        private static readonly Lazy<Ip2Location> _lazy = new Lazy<Ip2Location>(() => new Ip2Location());
        
        /// <summary>
        /// The singleton Instance of Ip2Location
        /// </summary>
        public static Ip2Location Instance { get { return _lazy.Value; } }

        /// <summary>
        /// Private constructor called during Instance creation
        /// </summary>
        private Ip2Location()
        {
            using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
            {
                _ipAndLocations = (from b in context.GeoIps
                                   join c in context.GeoLocations on b.LocationId equals c.LocationId
                                   select new Tuple<GeoIp, GeoLocation, long, long>(b, c, new IPAddress(b.StartIpNumber.ToArray()).Address, new IPAddress(b.EndIpNumber.ToArray()).Address)).ToList<Tuple<GeoIp, GeoLocation, long, long>>();

            }
        }

        /// <summary>
        /// Get a <see cref="GeoLocation"/> object from a IP Address
        /// </summary>
        /// <param name="QueryIpAddress">An ipadress to query</param>
        /// <returns>The Geolocation object or null if not found</returns>
        public GeoLocation GetLocation(long QueryIpAddress)
        {
            //cache geolocation objects by ipaddress
            string cacheKey = String.Format("{0}",QueryIpAddress);
            if (_cache.Contains(cacheKey))
            {
                return (GeoLocation)_cache.Get(cacheKey);
            }

            foreach (Tuple<GeoIp, GeoLocation, long, long> k in _ipAndLocations)
            {
                if (QueryIpAddress >= k.Item3 && QueryIpAddress <= k.Item4)
                {
                    _cache.Add(cacheKey, k.Item2, new CacheItemPolicy());
                    return k.Item2;
                }
            }
            return null;
        }
    }
}
