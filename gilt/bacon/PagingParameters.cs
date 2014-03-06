using System;
using gilt.bacon.exceptions;

namespace gilt.bacon
{
    /// <summary>
    /// Paging Query Strings
    /// </summary>
    public sealed class PagingParameters
    {
        /// <summary>
        /// (?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public const string PAGING_PARAMETERS = "(?<Page>[0-9]*)/(?<PageSize>[0-9]*)";
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize {get {return _pageSize;}}
        /// <summary>
        /// Page Number
        /// </summary>
        public int PageNumber {get {return _pageNumber;}}

        private int _pageSize;
        private int _pageNumber;

        /// <summary>
        /// Create the paging parameters
        /// </summary>
        /// <param name="parameters">NancyFX paging parameters</param>
        public PagingParameters(dynamic parameters)
        {
            try
            {
                _pageSize = parameters.PageSize;
                _pageNumber = parameters.Page;
            }
            catch (Exception exc)
            {
                throw new InvalidPagingParameterException(String.Format("Invalid Paging Parameter"), exc);
            }
        }
    }
}
