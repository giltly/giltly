using gilt.dblinq.events;
using gilt.dblinq.proxy;
using gilt.dblinq.search;
using Nancy;
using Nancy.Security;
using SimileTimeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace gilt.bacon.modules
{
    public class EventModule : ModuleBase<IEventRepository, EventProxy>
    {
        private SearchProxy _activeSearch = null;

        public EventModule(IEventRepository eventRepository, ISearchRepository searchRepository) 
            : base (eventRepository, searchRepository)
        {
            this.RequiresAuthentication();            

            #region HTML Responses
            Get[GiltlyRoutes.EVENT_LIST] = parameters =>
            {
                return this.RenderView("EventList");
            };

            Get[GiltlyRoutes.EVENT_BY_IP_LIST] = parameters =>
            {
                return this.RenderView("EventByIpList");                
            };

            Get[GiltlyRoutes.EVENT_BY_COUNTRY_LIST] = parameters =>
            {                                
                return this.RenderView("EventByCountryList");                
            };

            Get[GiltlyRoutes.EVENT_BY_DESTINATIONPORT_LIST] = parameters =>
            {
                return this.RenderView("EventByDestinationPortList");                
            };

            Get[GiltlyRoutes.EVENT_BY_SOURCEPORT_LIST] = parameters =>
            {
                return this.RenderView("EventBySourcePortList");
            };

            Get[GiltlyRoutes.EVENT_TIMELINE] = parameters =>
            {
                return this.RenderView("EventTimeLine");
            };
            #endregion

            #region JSON Responses
            Get[ GiltlyRoutes.EVENT_PAGED] = parameters =>
            {
                int? activeSearchId = this.GetModel().UserModel.UserProxy.ActiveSearch;
                //if the user has selected a search then grab the settings
                if (null != activeSearchId)
                {
                    _activeSearch = searchRepository.FindBy(a => a.Id == activeSearchId).SingleOrDefault();
                }

                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;

                SortParameters sp = new SortParameters(this.Request.Query);

                IQueryable<EventProxy> filteredEvents = FilterEvents();
                int eventCount = filteredEvents.Count();
                var rows = filteredEvents.OrderBy(String.Format("{0} {1}", sp.SortName, sp.SortDirection)).Skip(page * pageSize).Take(pageSize);
                int totalPages = (int)Math.Ceiling((float)eventCount / (float)pageSize);

                return Response.AsJson(
                    new
                    {
                        Rows = rows,
                        NumberPages = totalPages
                    });
            };

            Get[GiltlyRoutes.EVENT_BY_ID] = parameters =>
            {
                decimal id = parameters.Id;
                return Response.AsJson(new object[] { _repository.FindBy(a => a.Id == id) });
            };

            Get[GiltlyRoutes.EVENT_PREVIOUS] = parameters =>
            {                
                int minutesBackStart = parameters.MinutesBackStart;
                int minutesDuration = parameters.MinutesDuration;
                DateTime startDate = DateTime.Now.Subtract(TimeSpan.FromMinutes(minutesBackStart));
                DateTime endDate = startDate.Add(TimeSpan.FromMinutes(minutesDuration));
                return Response.AsJson(new object[] { _repository.FindBy (a => a.TimeStamp >= startDate && a.TimeStamp <= endDate).Count() });
            };

            Get[GiltlyRoutes.EVENT_BY_IP_PAGED] = parameters =>
            {
                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;                

                var rows = _repository.GetEventsByIp().OrderByDescending(a => a.EventCount);
                int? eventCount = rows.Sum(a => a.EventCount);
                int totalPages = (int)Math.Ceiling((float)(rows.Count()) / (float)pageSize);
                return Response.AsJson(
                    new
                    {
                        Rows = rows.Skip(page * pageSize).Take(pageSize),
                        NumberPages = totalPages,
                        EventCount =  eventCount
                    });
            };

            Get[GiltlyRoutes.EVENT_BY_COUNTRY_PAGED] = parameters =>
            {
                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;

                var rows = _repository.GetEventsByCountry().OrderByDescending(a => a.CountryCount);
                int? eventCount = rows.Sum(a => a.CountryCount);
                int totalPages = (int)Math.Ceiling((float)(rows.Count()) / (float)pageSize);
                return Response.AsJson(
                    new
                    {
                        Rows = rows.Skip(page * pageSize).Take(pageSize),
                        NumberPages = totalPages,
                        EventCount =  eventCount
                    });
            };

            Get[GiltlyRoutes.EVENT_BY_DESTINATIONPORT_PAGED] = parameters =>
            {
                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;                

                var rows = _repository.GetEventsByDestinationPort().OrderByDescending(a => a.PortCount);
                int? eventCount = rows.Sum(a => a.PortCount);
                int totalPages = (int)Math.Ceiling((float)(rows.Count()) / (float)pageSize);
                return Response.AsJson(
                    new
                    {
                        Rows = rows.Skip(page * pageSize).Take(pageSize),
                        NumberPages = totalPages,
                        PortCount = eventCount
                    });
            };

            Get[GiltlyRoutes.EVENT_BY_SOURCEPORT_PAGED] = parameters =>
            {
                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;

                var rows = _repository.GetEventsBySourcePort().OrderByDescending(a => a.PortCount);
                int? eventCount = rows.Sum(a => a.PortCount);
                int totalPages = (int)Math.Ceiling((float)(rows.Count()) / (float)pageSize);
                return Response.AsJson(
                    new
                    {
                        Rows = rows.Skip(page * pageSize).Take(pageSize),
                        NumberPages = totalPages,
                        PortCount = eventCount
                    });
            };

            Get[GiltlyRoutes.EVENT_GEODATA] = parameters =>
            {
                return Response.AsJson(new
                {
                    GeoData = _repository.GetGeoPaths()
                });
            };

            Get[GiltlyRoutes.EVENT_TIMELINEDATA] = parameters =>
            {   
                //get the minimum and maximum date from the repo
                List<EventProxy> allEvents = _repository.GetAll().ToList();
                DateTime minTime = allEvents.Min(x => x.TimeStamp);
                DateTime maxTime = allEvents.Max(x => x.TimeStamp);

                //List<TimelineEvent> events = _repository.FindBy(a => (a.TimeStamp >=  DateTime.Parse("2011-11-12 00:00:00.000") && a.TimeStamp <= DateTime.Parse("2011-11-13 00:00:00.000"))).Select(b =>
                List<TimelineEvent> events = _repository.FindBy(a => (a.TimeStamp >=  minTime) && (a.TimeStamp <= maxTime)).Select(b =>
                new TimelineEvent
                {
                    Start = b.TimeStamp,
                    End = b.TimeStamp.AddMinutes(1),
                    IsDuration = true,
                    Title = b.SignatureClassification.Name,
                    Description = b.SignatureClassification.Name
                }).Take(100).ToList<TimelineEvent>();
                //the response is returned as text -- which is actually a bunch of javascript
                //the client does an eval() on it to display the graph
                return Response.AsText(new Timeline(events).GetTimeline());
            };
            #endregion
        }

        #region Search Methods
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
            return FilterPacketType(rows7);
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

        #endregion
    }
}
