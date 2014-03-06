using gilt.unified2.packets;

namespace gilt.unified2.database
{
    public sealed class DbEventIPV4Type2 : DbEventCommon<EventIPV4Type2>
    {
        public DbEventIPV4Type2(PacketBase pb) : base((EventIPV4Type2)pb)
        {            
        }
        public override void ToDatabase()
        {
        }
    }
}
