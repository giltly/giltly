using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;


namespace gilt.dblinq.events
{
    /// <summary>
    /// Event Repository
    /// </summary>
    public class EventRepository : GenericRepository<EventProxy>, IEventRepository, IEventDelete
    {
        /// <summary>
        /// Initialize the Event Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from e in DataContext.Events
                    //event comments
                    join ecom in DataContext.EventComments on
                        new { e.EventId, e.SensorId } equals new { ecom.EventId, ecom.SensorId } into evntcom
                    //event data
                    join d in DataContext.Datas on
                        new { e.EventId, e.SensorId } equals new { d.EventId, d.SensorId } into data
                    //signature
                    join sig in DataContext.Signatures on e.SignatureId equals sig.Id
                    //signature class
                    join sigc in DataContext.SignatureClassifications on sig.ClassificationId equals sigc.ClassificationId
                    //ipheader
                    join iph in DataContext.IPHeaders on
                        new { e.EventId, e.SensorId } equals new { iph.EventId, iph.SensorId } into ipheadouter
                        from iph in ipheadouter.DefaultIfEmpty()
                    //geolocation source
                    join gls in DataContext.GeoLocations on iph.IPSourceLocationId equals gls.LocationId into geolocationsourceouter
                        from gls in geolocationsourceouter.DefaultIfEmpty()
                    //geolocation destination
                    join gld in DataContext.GeoLocations on iph.IPDestinationLocationId equals gld.LocationId into geolocationdestinationouter
                        from gld in geolocationdestinationouter.DefaultIfEmpty()
                    //country code source
                    join ccs in DataContext.CountryCodes on gls.CountryCode equals ccs.ISO2 into geolocationcountrysourceouter
                        from ccs in geolocationcountrysourceouter.DefaultIfEmpty()
                    //country code destination
                    join ccd in DataContext.CountryCodes on gld.CountryCode equals ccd.ISO2 into geolocationcountrydestinationouter
                        from ccd in geolocationcountrydestinationouter.DefaultIfEmpty()
                    //tcpheader
                    join t in DataContext.TCPHeaders on
                        new { e.EventId, e.SensorId } equals new { t.EventId, t.SensorId } into tcpouter
                    from t in tcpouter.DefaultIfEmpty()
                    //flags
                    join f in DataContext.Flags on t.Flags equals f.Number into fla
                        from f in fla.DefaultIfEmpty()
                    //udpheader
                    join u in DataContext.UDPHeaders on
                        new { e.EventId, e.SensorId } equals new { u.EventId, u.SensorId } into udpouter
                    from u in udpouter.DefaultIfEmpty()
                    //icmpheader
                    join i in DataContext.ICMPHeaders on
                        new { e.EventId, e.SensorId } equals new { i.EventId, i.SensorId } into icmpouter
                    from i in icmpouter.DefaultIfEmpty()                             
                         select this.CreateEventProxy(e, evntcom.ToList(), iph, gls, gld, data.ToList(), t, u, i, f, sig, sigc, ccs, ccd)                           
                );
        }

        #region IEventRepository
        /// <summary>
        /// Get Event by Primary Key
        /// </summary>
        /// <param name="SensorId">The sensor Id</param>
        /// <param name="EventId">The event Id</param>
        /// <returns>List of events</returns>
        public EventProxy GetEventsByPK(int SensorId, int EventId)
        {
            return (from a in DataContext.Events
                    where a.SensorId == SensorId && a.EventId == EventId
                    select new EventProxy(a)).SingleOrDefault();
        }
        /// <summary>
        /// Get Events by Ip Address
        /// </summary>
        /// <returns>List of events grouped by Ip</returns>
        public IEnumerable<EventsByIpProxy> GetEventsByIp()
        {            
            return (from a in DataContext.EventsByIps
                    select new EventsByIpProxy(a));
        }
        /// <summary>
        /// Get Events by Country
        /// </summary>
        /// <returns>List of events grouped by Country</returns>
        public IEnumerable<EventsByCountryProxy> GetEventsByCountry()
        {
            return (from a in DataContext.EventsByCountries
                    select new EventsByCountryProxy(a));
        }
        /// <summary>
        /// Get Events by Destination Port
        /// </summary>
        /// <returns>List of events grouped by detination port</returns>
        public IEnumerable<UniqueDestinationPortProxy> GetEventsByDestinationPort()
        {
            return (from a in DataContext.UniqueDestinationPorts
                    select new UniqueDestinationPortProxy(a));
        }
        /// <summary>
        /// Get Events by Source Port
        /// </summary>
        /// <returns>List of events grouped by source port</returns>
        public IEnumerable<UniqueSourcePortProxy> GetEventsBySourcePort()
        {
            return (from a in DataContext.UniqueSourcePorts
                    select new UniqueSourcePortProxy(a));
        }
        /// <summary>
        /// Create an EventProxy from all sources
        /// </summary>
        /// <param name="E">Event</param>
        /// <param name="EC">EventComment</param>
        /// <param name="Iph">IPHeader</param>
        /// <param name="Gls">GeoLocation Source</param>
        /// <param name="Gld">GeoLocation Destination</param>
        /// <param name="D">Data</param>
        /// <param name="T">TCPHeader</param>
        /// <param name="U">UDPHeader</param>
        /// <param name="I">ICMPHeader</param>
        /// <param name="F">Flag</param>
        /// <param name="Sig">Signature</param>
        /// <param name="SigC">SignatureClassification</param>
        /// <param name="Ccs">CountryCode Source</param>
        /// <param name="Ccd">CountryCode Destination</param>
        /// <returns>The entire event</returns>
        public EventProxy CreateEventProxy(Event E, List<EventComment> EC, 
            IPHeader Iph, 
            GeoLocation Gls, GeoLocation Gld, List<Data> D, 
            TCPHeader T, UDPHeader U, ICMPHeader I, 
            Flag F, Signature Sig, SignatureClassification SigC, 
            CountryCode Ccs, CountryCode Ccd)
        {
            EventProxy ep = new EventProxy(E);
            ep.EventComments = EC.Select(x=> new EventCommentsProxy(x)).ToList();
            ep.IpHeader = new IpHeaderProxy(Iph);
            ep.GeoLocationSource = new GeoLocationProxy(Gls);
            ep.GeoLocationDestination = new GeoLocationProxy(Gld);
            ep.Data = D.Select(x => new DataProxy(x)).ToList();
            ep.TcpHeader = new TcpHeaderProxy(T);
            ep.UdpHeader = new UdpHeaderProxy(U);
            ep.IcmpHeader = new IcmpHeaderProxy(I);
            ep.Flag = new FlagProxy(F);
            ep.Signature = new SignatureProxy(Sig);
            ep.SignatureClassification = new SignatureClassificationProxy(SigC);
            ep.CountryCodeSource = new CountryCodeProxy(Ccs);
            ep.CountryCodeDestination = new CountryCodeProxy(Ccd);
            return ep;
        }
        #endregion

        #region IEventDelete
        /// <summary>
        /// Delete all event data
        /// </summary>
        public void DeleteAllEventData()
        {
            DataContext.DeleteEventData();
        }        
        /// <summary>
        /// Delete all geolocation data
        /// </summary>
        public void DeleteAllGeoData()
        {
            DataContext.DeleteGeoData();
        }
        /// <summary>
        /// Delete all snort data
        /// </summary>
        public void DeleteAllSnortData()
        {
            DataContext.DeleteSnortData();
        }
        /// <summary>
        /// Delete all log history data
        /// </summary>
        public void DeleteAllLogHistoryData()
        {
            DataContext.DeleteLogData();
        }
        /// <summary>
        /// Delete all non geolocation data
        /// </summary>
        public void DeleteAllNonGeoData()
        {
            this.DeleteAllEventData();
            this.DeleteAllLogHistoryData();
            this.DeleteAllSnortData();
        }
        /// <summary>
        /// Delete everything
        /// </summary>
        public void DeleteAllData()
        {
            this.DeleteAllNonGeoData();
            this.DeleteAllGeoData();
        }
        #endregion

        #region IGeoEvent
        /// <summary>
        /// Get Source and Destination Locations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GeoDataProxy> GetGeoPaths()
        {            
            return this.FindBy(a => 1 == 1).Select(x => new GeoDataProxy(x.GeoLocationSource, x.GeoLocationDestination));
        }
        #endregion

        #region IEventRepository CRUD
        /// <summary>
        /// Update a Event
        /// </summary>
        /// <param name="EventProxy">Event to Update</param>
        public override void Update(EventProxy EventProxy)
        {
            Event existingEvent = (from s in DataContext.Events where s.SensorId == EventProxy.SensorId && s.EventId == EventProxy.EventId select s).Single();
            existingEvent.SensorId = EventProxy.SensorId;
            existingEvent.EventId = EventProxy.EventId;
            existingEvent.SignatureId = EventProxy.SignatureId;
            existingEvent.TimeStamp = EventProxy.TimeStamp;            
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add an Event
        /// </summary>
        /// <param name="EventProxy">Event to Add</param>
        public override void Add(EventProxy EventProxy)
        {
            Event newEvent = new Event();
            newEvent.SensorId = EventProxy.SensorId;
            newEvent.EventId = EventProxy.EventId;
            newEvent.SignatureId = EventProxy.SignatureId;
            newEvent.TimeStamp = EventProxy.TimeStamp;
            DataContext.Events.InsertOnSubmit(newEvent);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a Event
        /// </summary>
        /// <param name="EventProxy">Event to Delete</param>
        public override void Delete(EventProxy EventProxy)
        {
            Event evts = (from s in DataContext.Events where s.SensorId == EventProxy.SensorId && s.EventId == EventProxy.EventId select s).Single();
            DataContext.Events.DeleteOnSubmit(evts);
            DataContext.SubmitChanges();
        }
        #endregion
    }
}
