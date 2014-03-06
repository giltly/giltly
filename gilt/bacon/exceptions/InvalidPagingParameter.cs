using System;

namespace gilt.bacon.exceptions
{
    /// <summary>
    /// Invalid Paging Exceptions
    /// </summary>
    public class InvalidPagingParameterException : Exception
    {
        /// <summary>
        /// Invalid Paging Exceptions
        /// </summary>
        /// <param name="Message">Message</param>
        /// <param name="Exc">Exception</param>
        public InvalidPagingParameterException(string Message, Exception Exc)
            : base(Message, Exc)
        {
        }
    }
}
