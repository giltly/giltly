using gilt.dblinq.proxy;
using System.Collections.Generic;
using System.Data.Linq;

namespace gilt.dblinq.search
{
    /// <summary>
    /// Search for events
    /// </summary>
    public interface ISearchEvent
    {
        /// <summary>
        /// Get distinct source ports
        /// </summary>
        /// <returns>List of distinct source ports</returns>
        IEnumerable<int> GetDistinctSourcePorts();
        /// <summary>
        /// Get distinct destination ports
        /// </summary>
        /// <returns>List of distinct destination ports</returns>
        IEnumerable<int> GetDistinctDestinationPorts();
        /// <summary>
        /// Get distinct source ip addresses
        /// </summary>
        /// <returns>List of distinct source ip addresses</returns>
        IEnumerable<Binary> GetDistinctSourceIP();
        /// <summary>
        /// Get distinct destination ip addresses
        /// </summary>
        /// <returns>List of distinct destination ip addresses</returns>
        IEnumerable<Binary> GetDistinctDestinationIP();
        /// <summary>
        /// Get signatureclassifications
        /// </summary>
        /// <returns>List of signature classifications</returns>
        IEnumerable<SignatureClassificationProxy> GetSignatureClassifications();
        /// <summary>
        /// Get Signatures
        /// </summary>
        /// <returns>List of Signatures</returns>
        IEnumerable<SignatureProxy> GetSignatures();
    }
}
