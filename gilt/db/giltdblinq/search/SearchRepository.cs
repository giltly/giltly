using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.search
{
    /// <summary>
    /// Search Repository
    /// </summary>
    public class SearchRepository : GenericRepository<SearchProxy>, ISearchRepository
    {
        /// <summary>
        /// Initialize Search Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from s in DataContext.Searches select new SearchProxy(s));           
        }

        #region ISearchRepository CRUD
        /// <summary>
        /// Update a Search
        /// </summary>
        /// <param name="SearchProxy">Search to Update</param>
        public override void Update(SearchProxy SearchProxy)
        {
            byte[] searchSourceIp = (null == SearchProxy.SourceIp ? null : SearchProxy.SourceIp.ToArray());
            byte[] searchDestinationIp = (null == SearchProxy.DestinationIp ? null : SearchProxy.DestinationIp.ToArray());

            Search existingSearch = (from s in DataContext.Searches where s.Id == SearchProxy.Id select s).Single();
            existingSearch.Name = SearchProxy.Name;
            existingSearch.SourceIp = (null == searchSourceIp ? null : (   0 == searchSourceIp.Length ? null : SearchProxy.SourceIp));
            existingSearch.DestinationIp = (null == searchDestinationIp ? null : (0 == searchDestinationIp.Length ? null : SearchProxy.DestinationIp)); 
            existingSearch.SignatureId = SearchProxy.SignatureId;
            existingSearch.SignatureClassificationId = SearchProxy.SignatureClassificationId;
            existingSearch.PacketType = SearchProxy.PacketType;
            existingSearch.SourcePort = SearchProxy.SourcePort;
            existingSearch.DestinationPort = SearchProxy.DestinationPort;
            existingSearch.StartSearch = SearchProxy.StartSearch;
            existingSearch.EndSearch = SearchProxy.EndSearch;                        
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a Search
        /// </summary>
        /// <param name="SearchProxy">Search to Add</param>
        public override void Add(SearchProxy SearchProxy)
        {
            Search newSearch = new Search();
            newSearch.Name = SearchProxy.Name;
            newSearch.CreatedBy = SearchProxy.CreatedBy;
            newSearch.CreatedOn = SearchProxy.CreatedOn;
            
            //store null not empty for the ipaddress
            newSearch.SourceIp = (null != SearchProxy.SourceIp ?  (0 == Convert.ToUInt64(SearchProxy.SourceIp.ToArray()) ? null :SearchProxy.SourceIp) : null);
            newSearch.DestinationIp = (null != SearchProxy.DestinationIp ? (0 == Convert.ToUInt64(SearchProxy.DestinationIp.ToArray()) ? null : SearchProxy.DestinationIp) : null); 
            
            newSearch.StartSearch = SearchProxy.StartSearch;
            newSearch.EndSearch = SearchProxy.EndSearch;

            newSearch.PacketType = SearchProxy.PacketType;
            newSearch.SignatureId = SearchProxy.SignatureId;

            newSearch.SourcePort = SearchProxy.SourcePort;
            newSearch.DestinationPort = SearchProxy.DestinationPort;

            DataContext.Searches.InsertOnSubmit(newSearch);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a Search
        /// </summary>
        /// <param name="SearchProxy">Search to Delete</param>
        public override void Delete(SearchProxy SearchProxy)
        {
            Search search = (from s in DataContext.Searches where s.Id == SearchProxy.Id select s).Single();
            DataContext.Searches.DeleteOnSubmit(search);
            DataContext.SubmitChanges();
        }

        #endregion

        #region ISearchEvent
        /// <summary>
        /// Get distinct source ports
        /// </summary>
        /// <returns>List of distinct source ports</returns>
        public IEnumerable<int> GetDistinctSourcePorts()
        {
            return (from a in DataContext.UniqueSourcePorts
                    select a.Port).Distinct();
        }
        /// <summary>
        /// Get distinct destination ports
        /// </summary>
        /// <returns>List of distinct destination ports</returns>
        public IEnumerable<int> GetDistinctDestinationPorts()
        {
            return (from a in DataContext.UniqueDestinationPorts
                    select a.Port).Distinct();
        }
        /// <summary>
        /// Get distinct source ip addresses
        /// </summary>
        /// <returns>List of distinct source ip addresses</returns>
        public IEnumerable<Binary> GetDistinctSourceIP()
        {
            return (from a in DataContext.IPHeaders
                    select a.IPSource).Distinct();
        }
        /// <summary>
        /// Get distinct destination ip addresses
        /// </summary>
        /// <returns>List of distinct destination ip addresses</returns>
        public IEnumerable<Binary> GetDistinctDestinationIP()
        {
            return (from a in DataContext.IPHeaders
                    select a.IPDestination).Distinct();
        }
        /// <summary>
        /// Get signatureclassifications
        /// </summary>
        /// <returns>List of signature classifications</returns>
        public IEnumerable<SignatureClassificationProxy> GetSignatureClassifications()
        {
            return (from a in DataContext.SignatureClassifications
                    select new SignatureClassificationProxy(a));
        }
        /// <summary>
        /// Get Signatures
        /// </summary>
        /// <returns>List of Signatures</returns>
        public IEnumerable<SignatureProxy> GetSignatures()
        {
            return (from a in DataContext.Signatures
                    select new SignatureProxy(a));
        }
        #endregion

    }
}
