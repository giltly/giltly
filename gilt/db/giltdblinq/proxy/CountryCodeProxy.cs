
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Country Code Proxy
    /// </summary>
    public sealed class CountryCodeProxy : ProxyBase
    {
        /// <summary>
        /// The id
        /// </summary>
        public int Id { get; set; }        
        /// <summary>
        /// Two character country code
        /// </summary>
        public string ISO2 { get; set; }
        /// <summary>
        /// Three character country code
        /// </summary>
        public string ISO3 { get; set; }
        /// <summary>
        /// Country Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Printable country name
        /// </summary>
        public string PrintableName { get; set; }
        /// <summary>
        /// Country Code
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Create a country code proxy from a country code
        /// </summary>
        /// <param name="CC">The country code</param>
        public CountryCodeProxy(CountryCode CC)
        {
            if (null != CC)
            {
                Id = CC.Id;
                ISO2 = CC.ISO2;
                ISO3 = CC.ISO3;
                Name = CC.Name;
                PrintableName = CC.PrintableName;
                Code = CC.Code;                
            }
        }
    }
}
