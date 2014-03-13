using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// IcmpHeader Repository
    /// </summary>
    public class IcmpHeadersRepository : GenericRepository<IcmpHeaderProxy>, IIcmpHeadersRepository
    {
        /// <summary>
        /// Initialize the ICMPHeader Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from s in DataContext.ICMPHeaders select new IcmpHeaderProxy(s));           
        }

        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>IcmpHeader</returns>
        public IcmpHeaderProxy GetEventsByPK(int SensorId, int EventId)
        {
            return (from a in DataContext.ICMPHeaders
                    where a.SensorId == SensorId && a.EventId == EventId
                    select new IcmpHeaderProxy(a)).SingleOrDefault();
        }

        #region IIcmpHeadersRepository CRUD
        /// <summary>
        /// Update a ICmpHeader
        /// </summary>
        /// <param name="IcmpHeaderProxy">IcmpHeader to Update</param>
        public override void Update(IcmpHeaderProxy IcmpHeaderProxy)
        {
            ICMPHeader existingIcmpHeader = (from s in DataContext.ICMPHeaders where s.Id == IcmpHeaderProxy.Id select s).Single();
            existingIcmpHeader.SensorId = IcmpHeaderProxy.SensorId;
            existingIcmpHeader.EventId = IcmpHeaderProxy.EventId;
            existingIcmpHeader.Type = IcmpHeaderProxy.Type;
            existingIcmpHeader.Code = IcmpHeaderProxy.Code;
            existingIcmpHeader.Checksum = IcmpHeaderProxy.Checksum;
            existingIcmpHeader.ICMPId = IcmpHeaderProxy.ICMPId;
            existingIcmpHeader.ICMPSequence = IcmpHeaderProxy.ICMPSequence;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a IcmpHeader
        /// </summary>
        /// <param name="IcmpHeaderProxy">IcmpHeader to Add</param>
        public override void Add(IcmpHeaderProxy IcmpHeaderProxy)
        {
            ICMPHeader newIcmpHeader = new ICMPHeader();
            newIcmpHeader.SensorId = IcmpHeaderProxy.SensorId;
            newIcmpHeader.EventId = IcmpHeaderProxy.EventId;
            newIcmpHeader.Type = IcmpHeaderProxy.Type;
            newIcmpHeader.Code = IcmpHeaderProxy.Code;
            newIcmpHeader.Checksum = IcmpHeaderProxy.Checksum;
            newIcmpHeader.ICMPId = IcmpHeaderProxy.ICMPId;
            newIcmpHeader.ICMPSequence = IcmpHeaderProxy.ICMPSequence;
            DataContext.ICMPHeaders.InsertOnSubmit(newIcmpHeader);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a IcmpHeader
        /// </summary>
        /// <param name="IcmpHeaderProxy">IcmpHeader to Delete</param>
        public override void Delete(IcmpHeaderProxy IcmpHeaderProxy)
        {
            ICMPHeader icmpHeader = (from s in DataContext.ICMPHeaders where s.Id == IcmpHeaderProxy.Id select s).Single();
            DataContext.ICMPHeaders.DeleteOnSubmit(icmpHeader);
            DataContext.SubmitChanges();
        }
        #endregion

    }
}
