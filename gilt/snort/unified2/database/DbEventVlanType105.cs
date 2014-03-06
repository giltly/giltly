using gilt.unified2.packets;

namespace gilt.unified2.database
{
    public sealed class DbEventVlanType105 : DbEventCommon<EventVlanType105>
    {
        public DbEventVlanType105(PacketBase pb)
            : base((EventVlanType105)pb)
        {
            
        }

        public override void ToDatabase()
        {
        }
    }
}
