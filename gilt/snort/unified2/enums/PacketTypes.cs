
namespace gilt.unified2.enums
{
    /// <summary>
    /// Unified 2 Packet Types found in <see cref="gilt.unified2.packets.Unified2Header"/>
    /// </summary>
    public enum PacketTypes
    {
        /// <summary>
        /// Unified2 Packet
        /// </summary>
        UNIFIED2_PACKET = 2,
        /// <summary>
        /// IPV6 Event
        /// </summary>
        UNIFIED2_IDS_EVENT_IPV6 = 99,
        /// <summary>
        /// MPLS Event
        /// </summary>
        UNIFIED2_IDS_EVENT_MPLS = 100,
        /// <summary>
        /// Unified2 IDS Event      (Version 2)
        /// </summary>
        UNIFIED2_IDS_EVENT_IPV6_MPLS = 104,
        /// <summary>
        /// Unified2 IDS Event IP6  (Version 2)
        /// </summary>
        UNIFIED2_IDS_EVENT_VLAN = 105,
        /// <summary>
        /// Unified2 Extra Data
        /// </summary>
        UNIFIED2_EXTRA_DATA = 110,
    }
}
