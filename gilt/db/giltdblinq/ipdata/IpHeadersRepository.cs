using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// IpHeader Repository
    /// </summary>
    public class IpHeadersRepository : GenericRepository<IpHeaderProxy>, IIPHeadersRepository
    {
        /// <summary>
        /// Initialize the IpHeader Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from s in DataContext.IPHeaders select new IpHeaderProxy(s));           
        }

        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>IcmpHeader</returns>
        public IpHeaderProxy GetEventsByPK(int SensorId, int EventId)
        {
            return (from a in DataContext.IPHeaders
                    where a.SensorId == SensorId && a.EventId == EventId
                    select new IpHeaderProxy(a)).SingleOrDefault();
        }

        #region IpHeaderRepository CRUD
        /// <summary>
        /// Update a IpHeader
        /// </summary>
        /// <param name="IpHeaderProxy">IpHeader to Update</param>
        public override void Update(IpHeaderProxy IpHeaderProxy)
        {
            IPHeader existingIpHeader = (from s in DataContext.IPHeaders where s.Id == IpHeaderProxy.Id select s).Single();
            existingIpHeader.SensorId = IpHeaderProxy.SensorId;
            existingIpHeader.EventId = IpHeaderProxy.EventId;
            existingIpHeader.IPSource = IpHeaderProxy.IPSource;
            existingIpHeader.IPDestination = IpHeaderProxy.IPDestination;
            existingIpHeader.IPSourceLocationId = IpHeaderProxy.IPSourceLocationId;
            existingIpHeader.IPDestinationLocationId = IpHeaderProxy.IPDestinationLocationId;
            existingIpHeader.Version = IpHeaderProxy.Version;
            existingIpHeader.HeaderLength = IpHeaderProxy.HeaderLength;
            existingIpHeader.TOS = IpHeaderProxy.TOS;
            existingIpHeader.Length = IpHeaderProxy.Length;
            existingIpHeader.Identification = IpHeaderProxy.Identification;
            existingIpHeader.Flags = IpHeaderProxy.Flags;
            existingIpHeader.Offset = IpHeaderProxy.Offset;
            existingIpHeader.TTL = IpHeaderProxy.TTL;
            existingIpHeader.ProtocolId = IpHeaderProxy.ProtocolId;
            existingIpHeader.CheckSum = IpHeaderProxy.CheckSum;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a IpHeader
        /// </summary>
        /// <param name="IpHeaderProxy">IpHeader to Add</param>
        public override void Add(IpHeaderProxy IpHeaderProxy)
        {
            IPHeader newIpHeader = new IPHeader();
            newIpHeader.SensorId = IpHeaderProxy.SensorId;
            newIpHeader.EventId = IpHeaderProxy.EventId;
            newIpHeader.IPSource = IpHeaderProxy.IPSource;
            newIpHeader.IPDestination = IpHeaderProxy.IPDestination;
            newIpHeader.IPSourceLocationId = IpHeaderProxy.IPSourceLocationId;
            newIpHeader.IPDestinationLocationId = IpHeaderProxy.IPDestinationLocationId;
            newIpHeader.Version = IpHeaderProxy.Version;
            newIpHeader.HeaderLength = IpHeaderProxy.HeaderLength;
            newIpHeader.TOS = IpHeaderProxy.TOS;
            newIpHeader.Length = IpHeaderProxy.Length;
            newIpHeader.Identification = IpHeaderProxy.Identification;
            newIpHeader.Flags = IpHeaderProxy.Flags;
            newIpHeader.Offset = IpHeaderProxy.Offset;
            newIpHeader.TTL = IpHeaderProxy.TTL;
            newIpHeader.ProtocolId = IpHeaderProxy.ProtocolId;
            newIpHeader.CheckSum = IpHeaderProxy.CheckSum;
            DataContext.IPHeaders.InsertOnSubmit(newIpHeader);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a IpHeader
        /// </summary>
        /// <param name="IpHeaderProxy">IpHeader to Delete</param>
        public override void Delete(IpHeaderProxy IpHeaderProxy)
        {
            IPHeader ipHeader = (from s in DataContext.IPHeaders where s.Id == IpHeaderProxy.Id select s).Single();
            DataContext.IPHeaders.DeleteOnSubmit(ipHeader);
            DataContext.SubmitChanges();
        }
        #endregion

    }
}
