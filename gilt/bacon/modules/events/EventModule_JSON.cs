using gilt.dblinq.events;
using gilt.dblinq.proxy;
using Nancy;
using SimileTimeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace gilt.bacon.modules.events
{
    public partial class EventModule : ModuleBase<IEventRepository, EventProxy>
    {
        /// <summary>
        /// JSON responses for event data
        /// </summary>
        protected override void JsonResponses()
        {
            Get[GiltlyRoutes.EVENT_PAGED] = parameters =>
            {
                int? activeSearchId = this.GetModel().UserModel.UserProxy.ActiveSearch;

                //if the user has selected a search then grab the settings
                if (null != activeSearchId)
                {
                    _activeSearch = _searchRepository.FindBy(a => a.Id == activeSearchId).SingleOrDefault();
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
                return Response.AsJson(
                    new
                    {
                        Rows = _repository.FindBy(a => a.EventId == id),
                        NumberPages = 1
                    });
            };

            Get[GiltlyRoutes.EVENT_PREVIOUS] = parameters =>
            {
                int minutesBackStart = parameters.MinutesBackStart;
                int minutesDuration = parameters.MinutesDuration;
                DateTime startDate = DateTime.Now.Subtract(TimeSpan.FromMinutes(minutesBackStart));
                DateTime endDate = startDate.Add(TimeSpan.FromMinutes(minutesDuration));
                return Response.AsJson(new object[] { _repository.FindBy(a => a.TimeStamp >= startDate && a.TimeStamp <= endDate).Count() });
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
                        EventCount = eventCount
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
                        EventCount = eventCount
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
                
                List<TimelineEvent> events = _repository.FindBy(a => (a.TimeStamp >= minTime) && (a.TimeStamp <= maxTime)).Select(b =>
                new TimelineEvent
                {
                    Start = b.TimeStamp,
                    End = b.TimeStamp.AddMinutes(1),
                    IsDuration = true,
                    Title = b.SignatureClassification.Name,
                    Description = b.SignatureClassification.Name,
                    Link = String.Format(@"/Event/View/{0}", b.Id)
                }).Take(100).ToList<TimelineEvent>();

                //create the bands using the minTime as the start point
                List<BandInfo> bands = new List<BandInfo>()
                {
                    new BandInfo(minTime, 20, DateTimeFormats.MINUTE, 50),
                    new BandInfo(minTime, 30, DateTimeFormats.HOUR, 200),
                    new BandInfo(minTime, 20, DateTimeFormats.DAY, 400)
                };                               
                //create a timeline class
                Timeline tl = new Timeline(events, bands);                
                //return the band information and event data as seperate json strings
                return Response.AsJson(
                    new
                    {
                        EventData = tl.GetEventDataJson(),
                        BandInfo = tl.GetBandInfoJson(),                        
                    });                
            };
        }
    }
}
