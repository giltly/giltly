using gilt.unified2.packets;
using PacketDotNet;

namespace gilt.unified2.database
{
    public sealed class DbEventVlanType104 : DbEventCommon<EventVlanType104>
    {
        public DbEventVlanType104(PacketBase pb)
            : base((EventVlanType104)pb)
        {            
        }

        public override void ToDatabase()
        {            
            decimal eventId = this.InsertNewEvent();

            switch ((IPProtocolType)_packet.Protocol)
            {
                case IPProtocolType.IP:
                    break;
                case IPProtocolType.TCP:
                    {
                        this.CreateUpdateTcpHeader(_packet.SensorId, eventId, new TcpPacket(_packet.SourcePort,_packet.DestinationPort));                                
                    }
                    break;
                case IPProtocolType.ICMP:
                    {
                        this.CreateUpdateIpHeader(_packet.SensorId, eventId, null, null);  
                    }
                    break;
                case IPProtocolType.UDP:
                    {
                        this.CreateUpdateUdpHeader(_packet.SensorId, eventId, new UdpPacket(_packet.SourcePort, _packet.DestinationPort));                                
                    }
                    break;
                default:
                    break;
            }            
        }
    }
}
