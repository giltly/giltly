using gilt.dblinq;
using gilt.dblinq.events;
using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqEventRepository : GenericRepository<EventProxy>, IEventRepository
    {
        private IEnumerable<EventsByIpProxy> _eventsByIp;
        private IEnumerable<UniqueSourcePortProxy> _sourcePorts;
        private IEnumerable<UniqueDestinationPortProxy> _destinationPorts;
        private IEnumerable<EventsByCountryProxy> _eventsByCountry;
        private IEnumerable<GeoDataProxy> _geoEvents;

        public MoqEventRepository()
        {
            _eventsByIp = new List<EventsByIpProxy>
            {
                new EventsByIpProxy(new EventsByIp { Ip = "127.0.0.1", EventCount = 100 })
            };
            _eventsByCountry = new List<EventsByCountryProxy>
            {
                new EventsByCountryProxy(new EventsByCountry { CountryCode = "127.0.0.1", CountryCode3 = "", CountryCount = 100 })
            };
            _geoEvents = new List<GeoDataProxy>
            {
                new GeoDataProxy(new GeoLocationProxy(new GeoLocation ()), 
                                 new GeoLocationProxy(new GeoLocation ()))
            };
            _sourcePorts = new List<UniqueSourcePortProxy>
            {
                new UniqueSourcePortProxy(new UniqueSourcePort { Port = 80, PortCount = 100 })
            };
            _destinationPorts = new List<UniqueDestinationPortProxy>
            {
                new UniqueDestinationPortProxy(new UniqueDestinationPort { Port = 80, PortCount = 100 })
            };
        }

        public EventProxy GetEventsByPK(int SensorId, int EventId)
        {
            return new EventProxy(new Event());
        }

        #region IEventDelete
        public void DeleteAllEventData()
        {
        }
        public void DeleteAllGeoData()
        {
        }
        public void DeleteAllSnortData()
        {
        }
        public void DeleteAllLogHistoryData()
        {
        }
        public void DeleteAllNonGeoData()
        {
        }
        public void DeleteAllData()
        { 
        }
        #endregion


        public IEnumerable<EventsByIpProxy> GetEventsByIp()
        {
            return _eventsByIp;
        }

        public IEnumerable<EventsByCountryProxy> GetEventsByCountry()
        {
            return _eventsByCountry;
        }

        public IEnumerable<UniqueDestinationPortProxy> GetEventsByDestinationPort()
        {
            return _destinationPorts;
        }

        public IEnumerable<UniqueSourcePortProxy> GetEventsBySourcePort()
        {
            return _sourcePorts;
        }

        public IEnumerable<GeoDataProxy> GetGeoPaths()
        {
            return _geoEvents;
        }

        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<EventProxy>)new List<EventProxy>
            {
                new EventProxy( new Event())
            }.AsEnumerable<EventProxy>().AsQueryable<EventProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<EventProxy> FindBy(Func<EventProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<EventProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(EventProxy proxy)
        {
        }
        public override void Add(EventProxy proxy)
        {
        }
        public override void Delete(EventProxy proxy)
        {
        }
        #endregion
    }
}
