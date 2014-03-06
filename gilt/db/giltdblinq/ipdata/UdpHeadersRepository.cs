using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// UdpHeader Repository
    /// </summary>
    public class UdpHeadersRepository : GenericRepository<UdpHeaderProxy>, IUdpHeadersRepository
    {
        /// <summary>
        /// Initialize the UdpHeader Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from s in DataContext.UDPHeaders select new UdpHeaderProxy(s));           
        }

        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>UdpHeader</returns>
        public UdpHeaderProxy GetEventsByPK(int SensorId, int EventId)
        {
            return (from a in DataContext.UDPHeaders
                    where a.SensorId == SensorId && a.EventId == EventId
                    select new UdpHeaderProxy(a)).SingleOrDefault();
        }

        #region IUdpHeadersRepository CRUD
        /// <summary>
        /// Update a UdpHeader
        /// </summary>
        /// <param name="UdpHeaderProxy">UdpHeader to Update</param>
        public override void Update(UdpHeaderProxy UdpHeaderProxy)
        {
            UDPHeader existingUdpHeader = (from s in DataContext.UDPHeaders where s.Id == UdpHeaderProxy.Id select s).Single();
            existingUdpHeader.SensorId = UdpHeaderProxy.SensorId;
            existingUdpHeader.EventId = UdpHeaderProxy.EventId;
            existingUdpHeader.SourcePort = UdpHeaderProxy.SourcePort;
            existingUdpHeader.DestinationPort = UdpHeaderProxy.DestinationPort;
            existingUdpHeader.Length = UdpHeaderProxy.Length;
            existingUdpHeader.CheckSum = UdpHeaderProxy.CheckSum;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a UdpHeader
        /// </summary>
        /// <param name="UdpHeaderProxy">UdpHeader to Add</param>
        public override void Add(UdpHeaderProxy UdpHeaderProxy)
        {
            UDPHeader newUdpHeader = new UDPHeader();
            newUdpHeader.SensorId = UdpHeaderProxy.SensorId;
            newUdpHeader.EventId = UdpHeaderProxy.EventId;
            newUdpHeader.SourcePort = UdpHeaderProxy.SourcePort;
            newUdpHeader.DestinationPort = UdpHeaderProxy.DestinationPort;
            newUdpHeader.Length = UdpHeaderProxy.Length;
            newUdpHeader.CheckSum = UdpHeaderProxy.CheckSum;
            DataContext.UDPHeaders.InsertOnSubmit(newUdpHeader);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a UdpHeader
        /// </summary>
        /// <param name="UdpHeaderProxy">UdpHeader to Delete</param>
        public override void Delete(UdpHeaderProxy UdpHeaderProxy)
        {
            UDPHeader udpHeader = (from s in DataContext.UDPHeaders where s.Id == UdpHeaderProxy.Id select s).Single();
            DataContext.UDPHeaders.DeleteOnSubmit(udpHeader);
            DataContext.SubmitChanges();
        }
        #endregion

    }
}
