
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// UniqueDestinationPort Proxy
    /// SQL View UniqueDestinationPort
    /// </summary>
    public sealed class UniqueDestinationPortProxy : ProxyBase
    {
        /// <summary>
        /// Port number
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Events on the Port
        /// </summary>
        public int? PortCount { get; set; }

        /// <summary>
        /// Create a UniqueDestinationPortProxy from a UniqueDestinationPort
        /// </summary>
        /// <param name="Usp">The UniqueDestinationPort</param>
        public UniqueDestinationPortProxy(UniqueDestinationPort Usp)
        {
            if (null != Usp)
            {
                Port = Usp.Port;
                PortCount = Usp.PortCount;
            }
        }

    }
}
