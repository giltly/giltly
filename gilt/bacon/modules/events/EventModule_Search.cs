using gilt.dblinq.events;
using gilt.dblinq.proxy;
using System.Linq;

namespace gilt.bacon.modules.events
{
    public partial class EventModule : ModuleBase<IEventRepository, EventProxy>
    {        
        private IQueryable<EventProxy> FilterEvents()
        {            
            IQueryable<EventProxy> events = _repository.FindBy(a => 1 == 1).AsQueryable();
            //do not bother doing any filtering if the user does not have anything selected
            if (null == _activeSearch)
            {
                return events;
            }
            IQueryable<EventProxy> rows1 = FilterSensor(events);
            IQueryable<EventProxy> rows2 = FilterSourceIp(rows1);
            IQueryable<EventProxy> rows3 = FilterSourcePort(rows2);
            IQueryable<EventProxy> rows4 = FilterDestinationIp(rows3);
            IQueryable<EventProxy> rows5 = FilterDestinationPort(rows4);
            IQueryable<EventProxy> rows6 = FilterStartTime(rows5);
            IQueryable<EventProxy> rows7 = FilterEndTime(rows6);
            IQueryable<EventProxy> rows8 = FilterSignature(rows7);
            IQueryable<EventProxy> rows9 = FilterSignatureClass(rows8);
            IQueryable<EventProxy> rows10 = FilterPacketType(rows9); 
            return FilterPacketType(rows10);
        }

        private IQueryable<EventProxy> FilterSensor(IQueryable<EventProxy> ep )
        {
            //TODO: SensorId in search
            if (null != _activeSearch)
            {
                return ep.Where(a => 0 == a.SensorId);
            }
            return ep;            
        }

        private IQueryable<EventProxy> FilterSourceIp(IQueryable<EventProxy> ep)
        {            
            if (null != _activeSearch.SourceIp)
            {
                return ep.Where(a => (null == a.IpHeader ? false : a.IpHeader.IPSource == _activeSearch.SourceIp));
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterSourcePort(IQueryable<EventProxy> ep)
        {            
            if (null != _activeSearch.SourcePort)
            {
                // if a packet type is selected then only filter that protocol
                if (null != _activeSearch.PacketType)
                {                    
                    switch (_activeSearch.PacketType)
                    {
                        case "TCP":
                            return ep.Where(a => (a.TcpHeader.SourcePort == _activeSearch.SourcePort));
                        case "UDP":
                            return ep.Where(a => (a.UdpHeader.SourcePort == _activeSearch.SourcePort));
                        default:
                            //icmp or other filter to tcp or udp
                            return ep.Where(a => (null == a.TcpHeader ? (null == a.UdpHeader ? false : a.UdpHeader.SourcePort == _activeSearch.SourcePort) : a.TcpHeader.SourcePort == _activeSearch.SourcePort));
                    }
                }
                else
                {
                    //filter either tcp or udp
                    return ep.Where(a => (null == a.TcpHeader ? (null == a.UdpHeader ? false : a.UdpHeader.SourcePort == _activeSearch.SourcePort) : a.TcpHeader.SourcePort == _activeSearch.SourcePort));
                }
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterDestinationIp(IQueryable<EventProxy> ep)
        {
            if (null != _activeSearch.DestinationIp)
            {
                return ep.Where(a => (null == a.IpHeader ? false : a.IpHeader.IPDestination == _activeSearch.DestinationIp));
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterDestinationPort(IQueryable<EventProxy> ep)
        {
            if (null != _activeSearch.DestinationPort)
            {
                // if a packet type is selected then only filter that protocol
                if (null != _activeSearch.PacketType)
                {
                    switch (_activeSearch.PacketType)
                    {
                        case "TCP":
                            return ep.Where(a => (a.TcpHeader.SourcePort == _activeSearch.DestinationPort));
                        case "UDP":
                            return ep.Where(a => (a.UdpHeader.SourcePort == _activeSearch.DestinationPort));
                        default:
                            //icmp or other filter to tcp or udp
                            return ep.Where(a => (null == a.TcpHeader ? (null == a.UdpHeader ? false : a.UdpHeader.DestinationPort == _activeSearch.DestinationPort) : a.TcpHeader.DestinationPort == _activeSearch.DestinationPort));
                    }
                }
                else
                {
                    //filter either tcp or udp
                    return ep.Where(a => (null == a.TcpHeader ? (null == a.UdpHeader ? false : a.UdpHeader.DestinationPort == _activeSearch.DestinationPort) : a.TcpHeader.DestinationPort == _activeSearch.DestinationPort));
                }
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterPacketType(IQueryable<EventProxy> ep)
        {
            if (null != _activeSearch.PacketType)
            {                
                //very strange  need to check that the primary key is present
                switch (_activeSearch.PacketType)
                {
                    case "TCP":
                        return ep.Where(a => (a.TcpHeader.Id > 0));   
                    case "UDP":
                        return ep.Where(a => (a.UdpHeader.Id > 0));  
                    case "ICMP":
                        return ep.Where(a => (a.IcmpHeader.Id > 0));
                }
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterStartTime(IQueryable<EventProxy> ep)
        {
            if (null != _activeSearch.StartSearch)
            {
                return ep.Where(a => (a.TimeStamp >= _activeSearch.StartSearch));
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterEndTime(IQueryable<EventProxy> ep)
        {
            if (null != _activeSearch.EndSearch)
            {
                return ep.Where(a => (a.TimeStamp <= _activeSearch.EndSearch));
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterSignature(IQueryable<EventProxy> ep)
        {
            if (null != _activeSearch.SignatureId)
            {
                return ep.Where(a => (a.SignatureId == _activeSearch.SignatureId));
            }
            return ep;
        }

        private IQueryable<EventProxy> FilterSignatureClass(IQueryable<EventProxy> ep)
        {
            if (null != _activeSearch.SignatureClassificationId)
            {
                return ep.Where(a => (a.SignatureClassification.Id == _activeSearch.SignatureClassificationId));
            }
            return ep;
        }
    }
}
