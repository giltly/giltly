using gilt.dblinq;
using gilt.dblinq.events;
using gilt.dblinq.ipdata;
using gilt.dblinq.proxy;
using gilt.dblinq.signatures;
using gilt.geo;
using gilt.unified2.packets;
using PacketDotNet;
using System.Linq;
using System.Net;

namespace gilt.unified2.database
{
    public abstract class DbPacketGenericBase<T>
    {
        protected T _packet;

        protected SignaturesRepository _signaturesRepo { get { return RepositorySingleton.Instance.SignaturesRepo; }}
        protected EventRepository _eventRepo { get { return RepositorySingleton.Instance.EventRepo; } }
        protected EventCommentsRepository _eventCommentsRepo { get { return RepositorySingleton.Instance.EventCommentsRepo; } }
        protected IpHeadersRepository _ipHeadersRepo { get { return RepositorySingleton.Instance.IpHeadersRepo; } }
        protected IcmpHeadersRepository _icmpHeadersRepo { get { return RepositorySingleton.Instance.IcmpHeadersRepo; } }
        protected UdpHeadersRepository _udpHeadersRepo { get { return RepositorySingleton.Instance.UdpHeadersRepo; } }
        protected DatasRepository _datasRepository { get { return RepositorySingleton.Instance.DatasRepository; } }
        protected TcpHeadersRepository _tcpHeadersRepository { get { return RepositorySingleton.Instance.TcpHeadersRepository; } }

        private DbPacketGenericBase()
        {
        }

        public DbPacketGenericBase(T pb)
            : this()
        {
            _packet = pb;
        }

        public abstract void ToDatabase();

        #region UdpHeader
        protected void CreateUpdateUdpHeader(decimal SensorId, decimal EventId, UdpPacket udpp)
        {            
            UdpHeaderProxy udph = _udpHeadersRepo.GetEventsByPK((int)SensorId, (int)EventId);
            if ((null != udph))
            {                
                udph.Length = udpp.Length;
                udph.CheckSum = udpp.Checksum;
                _udpHeadersRepo.Update(udph);
            }
            else
            {
                _udpHeadersRepo.Add(new UdpHeaderProxy(new UDPHeader()
                {
                    SensorId = SensorId,
                    EventId = EventId,
                    SourcePort = udpp.SourcePort,
                    DestinationPort = udpp.DestinationPort
                }));
            }
        }
        #endregion

        #region TcpHeader
        protected void CreateUpdateTcpHeader(decimal SensorId, decimal EventId, TcpPacket tcpp)
        {
            TcpHeaderProxy tch = _tcpHeadersRepository.GetEventsByPK((int)SensorId, (int)EventId);
            if ((null != tch))
            {
                tch.Sequence = tcpp.SequenceNumber;
                tch.ACK = tcpp.AcknowledgmentNumber;
                //TODO: 
                //tch.Offset
                //tch.Flags
                //tch.Reserved  
                tch.Window = tcpp.WindowSize;
                tch.CheckSum = tcpp.Checksum;
                tch.Urgent = tcpp.UrgentPointer;
                _tcpHeadersRepository.Update(tch);                
            }
            else
            {
                //not all infomration is not available right now
                //the packetdata [type 2] is needed to fill this in
                _tcpHeadersRepository.Add(new TcpHeaderProxy(new TCPHeader()
                {
                    SensorId = SensorId,
                    EventId = EventId,
                    SourcePort = tcpp.SourcePort,
                    DestinationPort = tcpp.DestinationPort
                }));
            }
        }
        #endregion

        #region ICMPHeader
        protected void CreateUpdateIcmpHeader(decimal SensorId, decimal EventId, ICMPv4Packet icmpp)
        {
            IcmpHeaderProxy icmph = _icmpHeadersRepo.GetEventsByPK((int)SensorId, (int)EventId);
            if ((null != icmph))
            {
                //icmph.Type = icmpp.TypeCode;
                //icmph.Code = icmpp.TypeCode;
                icmph.Checksum = icmpp.Checksum;
                icmph.ICMPId = icmpp.ID;
                icmph.ICMPSequence = icmpp.Sequence;
                _icmpHeadersRepo.Update(icmph);   
            }
            else
            {
                //not all infomration is not available right now
                //the packetdata [type 2] is needed to fill this in
                _icmpHeadersRepo.Add(new IcmpHeaderProxy(new ICMPHeader()
                {
                    SensorId = SensorId,
                    EventId = EventId,
                    Checksum = icmpp.Checksum,
                    ICMPId = icmpp.ID,
                    ICMPSequence = icmpp.Sequence
                }));                
            }
        }
        #endregion

        #region IPHeader
        protected void CreateUpdateIpHeader(decimal SensorId, decimal EventId, EthernetPacket ethernetPacket, IPv4Packet ipv4p)
        {
            IpHeaderProxy iphp = _ipHeadersRepo.GetEventsByPK((int)SensorId, (int)EventId);
            if ((null != iphp))
            {
                if (null != ethernetPacket)
                {
                    iphp.IPSource = ipv4p.SourceAddress.GetAddressBytes();
                    iphp.IPDestination = ipv4p.DestinationAddress.GetAddressBytes();

                    GeoLocation possibleSourceLocation = Ip2Location.Instance.GetLocation(new IPAddress(iphp.IPSource.ToArray()).Address);
                    if (null != possibleSourceLocation)
                    {
                        iphp.IPSourceLocationId = possibleSourceLocation.LocationId;
                    }
                    GeoLocation possibleDestinationLocation = Ip2Location.Instance.GetLocation(new IPAddress(iphp.IPDestination.ToArray()).Address);
                    if (null != possibleDestinationLocation)
                    {
                        iphp.IPDestinationLocationId = possibleDestinationLocation.LocationId;
                    }
                }
                if (null != ipv4p)
                {
                    iphp.Version = (byte)ipv4p.Version;
                    iphp.HeaderLength = (byte)ipv4p.HeaderLength;
                    iphp.TOS = (byte)ipv4p.TypeOfService;
                    iphp.Length = ipv4p.TotalLength;
                    iphp.Identification = ipv4p.Id;
                    iphp.Flags = (byte)ipv4p.FragmentFlags;
                    iphp.Offset = (byte)ipv4p.FragmentOffset;
                    iphp.TTL = (byte)ipv4p.TimeToLive;
                    iphp.ProtocolId = (byte)ipv4p.Protocol;
                    iphp.CheckSum = ipv4p.Checksum;
                }
                _ipHeadersRepo.Update(iphp);                                  
            }
            else
            {
                IPHeader iph = new IPHeader();
                iph.SensorId = SensorId;
                iph.EventId = EventId;
                if (null != ethernetPacket)
                {
                    iph.IPSource = ipv4p.SourceAddress.GetAddressBytes();
                    iph.IPDestination = ipv4p.DestinationAddress.GetAddressBytes();

                    GeoLocation possibleSourceLocation = Ip2Location.Instance.GetLocation(new IPAddress(iph.IPSource.ToArray()).Address);
                    if (null != possibleSourceLocation)
                    {
                        iph.IPSourceLocationId = possibleSourceLocation.LocationId;
                    }
                    GeoLocation possibleDestinationLocation = Ip2Location.Instance.GetLocation(new IPAddress(iph.IPDestination.ToArray()).Address);
                    if (null != possibleDestinationLocation)
                    {
                        iph.IPDestinationLocationId = possibleDestinationLocation.LocationId;
                    }
                }
                if (null != ipv4p)
                {
                    iph.Version = (byte)ipv4p.Version;
                    iph.HeaderLength = (byte)ipv4p.HeaderLength;
                    iph.TOS = (byte)ipv4p.TypeOfService;
                    iph.Length = ipv4p.TotalLength;
                    iph.Identification = ipv4p.Id;
                    iph.Flags = (byte)ipv4p.FragmentFlags;
                    iph.Offset = (byte)ipv4p.FragmentOffset;
                    iph.TTL = (byte)ipv4p.TimeToLive;
                    iph.ProtocolId = (byte)ipv4p.Protocol;
                    iph.CheckSum = ipv4p.Checksum;
                }
                _ipHeadersRepo.Add(new IpHeaderProxy(iph));  
            }
        }
        #endregion

    }
}
