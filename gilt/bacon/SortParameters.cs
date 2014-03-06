using System;
using gilt.bacon.exceptions;

namespace gilt.bacon
{
    /// <summary>
    /// Sorting Order 
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Ascending Sort
        /// </summary>
        ASC,
        /// <summary>
        /// Descending Sort
        /// </summary>
        DESC
    }

    /// <summary>
    /// Sorting Query Parameters
    /// </summary>
    public sealed class SortParameters
    {
        /// <summary>
        /// Name of the sort column
        /// </summary>
        public string SortName { get { return _sortName; } }
        /// <summary>
        /// Sort Order
        /// </summary>
        public SortOrder SortDirection {get {return _sortDirection;}}

        private string _sortName;
        private SortOrder _sortDirection;

        /// <summary>
        /// Create sorting Parameters from the query string
        /// </summary>
        /// <param name="query"></param>
        public SortParameters(Nancy.DynamicDictionary query)
        {
            try
            {                
                dynamic sortName;
                dynamic sortDirection;
                if (!query.TryGetValue("SortName", out sortName))
                {
                    throw new Exception();
                }
                if (!query.TryGetValue("SortDirection", out sortDirection))
                {
                    throw new Exception();
                }
                _sortName = sortName;
                Enum.TryParse<SortOrder>((string)sortDirection, out _sortDirection);
            }
            catch (Exception exc)
            {
                throw new InvalidSortingParameterException(String.Format("Invalid Sort Parameter"), exc);
            }
        }
    }
}
