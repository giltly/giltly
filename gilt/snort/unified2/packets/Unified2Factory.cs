using MiscUtil.IO;
using gilt.unified2.enums;

namespace gilt.unified2.packets
{
    /// <summary>
    /// Factory for returning <see cref="PacketBase"/> from a binary reader. 
    /// e.g. the unified2 file, and a recordtype
    /// </summary>
    public sealed class Unified2Factory
    {
        private EndianBinaryReader _binaryReader = null;
        private Unified2Header _header = null;

        /// <summary>
        /// Create a unified2 factory using a binary reader and a unified2 header
        /// </summary>
        /// <param name="BinaryReader">Endian aware binary reader</param>
        /// <param name="Header">Unified2Header that defines the packet length and type</param>
        public Unified2Factory(EndianBinaryReader BinaryReader, Unified2Header Header)
        {
            _binaryReader = BinaryReader;
            _header = Header;
        }

        /// <summary>
        /// Create a new <see cref="PacketBase"/> concrete based on the type of packet 
        /// </summary>
        /// <param name="RecordType">The type of unified2 packet</param>
        /// <returns></returns>
        public PacketBase CreatePacketParser(uint RecordType)
        {            
            switch (RecordType)
            {
                case (int)PacketTypes.UNIFIED2_PACKET:
                    return new PacketDataType2(_binaryReader, _header);
                case (int)PacketTypes.UNIFIED2_IDS_EVENT_IPV6_MPLS:
                    return new EventVlanType104(_binaryReader, _header);
                case (int)PacketTypes.UNIFIED2_IDS_EVENT_VLAN:
                    return new EventVlanType105(_binaryReader, _header);
                case (int)PacketTypes.UNIFIED2_EXTRA_DATA:
                    return new ExtraDataHeaderType110(_binaryReader, _header);
                default:
                    break;
            }
            return null;
        }
    }
}
