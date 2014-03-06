using System;
using System.Net;
using System.Data.Linq;
using gilt.dblinq;

namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Search Proxy
    /// </summary>
    public sealed class SearchProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Create by User Id
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Time Created On
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Source Ip
        /// </summary>
        public Binary SourceIp
        {
            get
            {
                return _sourceIp;
            }
            set 
            { 
                _sourceIp = value;
                _sourceIpString = (null == _sourceIp ? null : new IPAddress(_sourceIp.ToArray()).ToString());
            } 
        }
        private Binary _sourceIp;

        /// <summary>
        /// Destination Ip
        /// </summary>
        public Binary DestinationIp
        {
            get
            {
                return _destinationIp;                   
            }
            set 
            { 
                _destinationIp = value;
                _destinationIpString = (null == _destinationIp ? null : new IPAddress(_destinationIp.ToArray()).ToString()); 
            }
        }
        private Binary _destinationIp;

        /// <summary>
        /// Source Ip String
        /// </summary>
        public string SourceIpString 
        {
            get
            {
                return _sourceIpString;
            }
            set
            {
                _sourceIpString = value;
                _sourceIp = (String.IsNullOrWhiteSpace(_sourceIpString) ? null : IPAddress.Parse(_sourceIpString).GetAddressBytes());
            }
        }
        private string _sourceIpString = null;

        /// <summary>
        /// Destination Ip String
        /// </summary>
        public string DestinationIpString 
        {
            get
            {
                return _destinationIpString;                
            }
            set 
            { 
                _destinationIpString = value;
                _destinationIp = (String.IsNullOrWhiteSpace(_destinationIpString) ? null : IPAddress.Parse(_destinationIpString).GetAddressBytes());
            } 
        }
        private string _destinationIpString = null;

        /// <summary>
        /// Signature Id
        /// </summary>
        public decimal? SignatureId { get; set; }
        /// <summary>
        /// Signature Classification Id
        /// </summary>
        public decimal? SignatureClassificationId { get; set; }
        /// <summary>
        /// Packet Type
        /// </summary>
        public string PacketType { get; set; }
        /// <summary>
        /// Source Port
        /// </summary>
        public int? SourcePort { get; set; }
        /// <summary>
        /// Destination port
        /// </summary>
        public int? DestinationPort { get; set; }
        /// <summary>
        /// Start time of the search
        /// </summary>
        public DateTime? StartSearch { get; set; }
        /// <summary>
        /// End time of search
        /// </summary>
        public DateTime? EndSearch { get; set; }

        /// <summary>
        /// Default Constructor for Nancy Model .Bind()
        /// </summary>
        public SearchProxy()
        {
        }

        /// <summary>
        /// Create a SearchProxy from a Search
        /// </summary>
        /// <param name="S">The Search</param>
        public SearchProxy(Search S)  
        {
            if (null != S)
            {
                Id = S.Id;
                Name = S.Name;
                CreatedBy = S.CreatedBy;
                CreatedOn = S.CreatedOn;
                SourceIp = S.SourceIp;
                DestinationIp = S.DestinationIp;
                SignatureId = S.SignatureId;
                SignatureClassificationId = S.SignatureClassificationId;
                PacketType = S.PacketType;
                SourcePort = S.SourcePort;
                DestinationPort = S.DestinationPort;
                StartSearch = S.StartSearch;
                EndSearch = S.EndSearch;    
            }
        }
    }
}
