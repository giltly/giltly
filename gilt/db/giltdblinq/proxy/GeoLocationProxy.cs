
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Geolocation proxy
    /// </summary>
    public sealed class GeoLocationProxy : ProxyBase
    { 
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Location Id
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// Region Code
        /// </summary>
        public string RegionCode { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Postal Code
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double? Longitude { get; set; }
        /// <summary>
        /// Metro Administrative Area Code
        /// </summary>
        public int? DmaCode { get; set; }
        /// <summary>
        /// Area Code
        /// </summary>
        public int? AreaCode { get; set; }

        /// <summary>
        /// Create a geolcation proxy from a geolocation
        /// </summary>
        /// <param name="Gl">The geolocation</param>
        public GeoLocationProxy(GeoLocation Gl)
        {
            if (null != Gl)
            {
                Id = Gl.Id;
                LocationId = Gl.LocationId;
                //The fam-fam icons need lower case names
                //CountryCode = gl.CountryCode.ToLower();
                CountryCode = Gl.CountryCode;
                RegionCode = Gl.RegionCode;
                City = Gl.City;
                PostalCode = Gl.PostalCode;
                Latitude = Gl.Latitude;
                Longitude = Gl.Longitude;
                DmaCode = Gl.DmaCode;
                AreaCode = Gl.AreaCode;
            }
        }
    }
}
