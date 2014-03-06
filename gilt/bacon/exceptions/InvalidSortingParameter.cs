using System;

namespace gilt.bacon.exceptions
{
    /// <summary>
    /// Sorting Paging Parameters
    /// </summary>
    public class InvalidSortingParameterException : Exception
    {
        /// <summary>
        /// Invalid Paging Parameters
        /// </summary>
        /// <param name="Message">Message</param>
        /// <param name="Exc">Exception</param>
        public InvalidSortingParameterException(string Message, Exception Exc)
            : base(Message, Exc)
        {
        }
    }
}
