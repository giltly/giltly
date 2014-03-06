using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.unified2.packets;
using gilt.util;
using PacketDotNet;
using System.Linq;
using System.Transactions;

namespace gilt.unified2.database
{
    public sealed class DbPacketDataType2 : DbPacketGenericBase<PacketDataType2>
    {
        public DbPacketDataType2(PacketBase pb)
            : base((PacketDataType2)pb)
        {
        }

        public override void ToDatabase()
        {
            if (LinkLayers.Ethernet == (LinkLayers)_packet.LinkType)
            {
                EthernetPacket ethernetPacket = (EthernetPacket)Packet.ParsePacket((LinkLayers)_packet.LinkType, _packet.PacketData);                
                EventProxy prevEvent = _eventRepo.GetEventsByPK((int)_packet.SensorId, (int)_packet.EventId);

                if (null != prevEvent)
                {
                    Packet foundPacket = Packet.ParsePacket((LinkLayers)_packet.LinkType, _packet.PacketData);
                    Packet packetSearch = foundPacket;
                    while (null != (packetSearch = packetSearch.PayloadPacket))
                    {
                        foundPacket = packetSearch;
                        if (null != foundPacket)
                        {
                            string packetTypeName = foundPacket.GetType().Name;
                            switch (packetTypeName)
                            {
                                case "IPv4Packet":
                                    this.CreateUpdateIpHeader(_packet.SensorId, prevEvent.EventId, ethernetPacket, (IPv4Packet)foundPacket);
                                    break;
                                case "ICMPv4Packet":
                                    this.CreateUpdateIcmpHeader(_packet.SensorId, prevEvent.EventId, (ICMPv4Packet)foundPacket);
                                    break;
                                case "TcpPacket":
                                    this.CreateUpdateTcpHeader(_packet.SensorId, prevEvent.EventId, (TcpPacket)foundPacket);
                                    break;
                                case "UdpPacket":
                                    this.CreateUpdateUdpHeader(_packet.SensorId, prevEvent.EventId, (UdpPacket)foundPacket);
                                    break;
                                default:
                                    int a = 1;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
