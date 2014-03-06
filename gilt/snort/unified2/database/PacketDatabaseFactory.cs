using gilt.unified2.packets;

namespace gilt.unified2.database
{
    public sealed class PacketDatabaseFactory
    {
        public static void InsertDbPacket(PacketBase packet)
        {             
            if (packet is EventIPV4Type2)
            {
                var pack = new DbEventIPV4Type2((EventIPV4Type2)packet);
                pack.ToDatabase();
            }
            else if (packet is EventVlanType104)
            {
                var pack = new DbEventVlanType104((EventVlanType104)packet);
                pack.ToDatabase();
            }
            else if (packet is EventVlanType105)
            {
                var pack = new DbEventVlanType105((EventVlanType105)packet);
                pack.ToDatabase();
            }
            else if (packet is ExtraDataHeaderType110)
            {
                var pack = new DbExtraDataHeaderType110((ExtraDataHeaderType110)packet);
                pack.ToDatabase();
            }
            else if (packet is PacketDataType2)
            {
                var pack = new DbPacketDataType2((PacketDataType2)packet);
                pack.ToDatabase();
            }
        }
    }
}
