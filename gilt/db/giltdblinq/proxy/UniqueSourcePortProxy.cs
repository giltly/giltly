
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// UniqueSourcePort Proxy
    /// SQL View UniqueSourcePort
    /// </summary>
    public sealed class UniqueSourcePortProxy : ProxyBase
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
        /// Create a UniqueSourcePortProxy from a UniqueSourcePort
        /// </summary>
        /// <param name="Usp">The UniqueSourcePort</param>
        public UniqueSourcePortProxy(UniqueSourcePort Usp)
        {
            if (null != Usp)
            {
                Port = Usp.Port;
                PortCount = Usp.PortCount;                
            }
        }

    }
}
