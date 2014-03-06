using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// TcpHeader Repository
    /// </summary>
    public class TcpHeadersRepository : GenericRepository<TcpHeaderProxy>, ITcpHeadersRepository
    {        
        /// <summary>
        /// Initialize the TcpHeader Repository
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from s in DataContext.TCPHeaders select new TcpHeaderProxy(s));           
        }

        /// <summary>
        /// Get Events by Primary Key
        /// </summary>
        /// <param name="SensorId">Sensor Id</param>
        /// <param name="EventId">Event Id</param>
        /// <returns>TcpHeader</returns>
        public TcpHeaderProxy GetEventsByPK(int SensorId, int EventId)
        {
            return (from a in DataContext.TCPHeaders
                    where a.SensorId == SensorId && a.EventId == EventId
                    select new TcpHeaderProxy(a)).SingleOrDefault();
        }

        #region ITcpHeadersRepository CRUD
        /// <summary>
        /// Update a TcpHeader
        /// </summary>
        /// <param name="TcpHeaderProxy">TcpHeader to Update</param>
        public override void Update(TcpHeaderProxy TcpHeaderProxy)
        {
            TCPHeader existingTcpHeader = (from s in DataContext.TCPHeaders where s.Id == TcpHeaderProxy.Id select s).Single();
            existingTcpHeader.SensorId = TcpHeaderProxy.SensorId;
            existingTcpHeader.EventId = TcpHeaderProxy.EventId;
            existingTcpHeader.SourcePort = TcpHeaderProxy.SourcePort;
            existingTcpHeader.DestinationPort = TcpHeaderProxy.DestinationPort;
            existingTcpHeader.Sequence = TcpHeaderProxy.Sequence;
            existingTcpHeader.ACK = TcpHeaderProxy.ACK;
            existingTcpHeader.Offset = TcpHeaderProxy.Offset;
            existingTcpHeader.Reserved = TcpHeaderProxy.Reserved;
            existingTcpHeader.Flags = TcpHeaderProxy.Flags;
            existingTcpHeader.Window = TcpHeaderProxy.Window;
            existingTcpHeader.CheckSum = TcpHeaderProxy.CheckSum;
            existingTcpHeader.Urgent = TcpHeaderProxy.Urgent;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a TcpHeader
        /// </summary>
        /// <param name="TcpHeaderProxy">TcpHeader to Add</param>
        public override void Add(TcpHeaderProxy TcpHeaderProxy)
        {
            TCPHeader newTcpHeader = new TCPHeader();
            newTcpHeader.SensorId = TcpHeaderProxy.SensorId;
            newTcpHeader.EventId = TcpHeaderProxy.EventId;
            newTcpHeader.SourcePort = TcpHeaderProxy.SourcePort;
            newTcpHeader.DestinationPort = TcpHeaderProxy.DestinationPort;
            newTcpHeader.Sequence = TcpHeaderProxy.Sequence;
            newTcpHeader.ACK = TcpHeaderProxy.ACK;
            newTcpHeader.Offset = TcpHeaderProxy.Offset;
            newTcpHeader.Reserved = TcpHeaderProxy.Reserved;
            newTcpHeader.Flags = TcpHeaderProxy.Flags;
            newTcpHeader.Window = TcpHeaderProxy.Window;
            newTcpHeader.CheckSum = TcpHeaderProxy.CheckSum;
            newTcpHeader.Urgent = TcpHeaderProxy.Urgent;
            DataContext.TCPHeaders.InsertOnSubmit(newTcpHeader);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a TcpHeader
        /// </summary>
        /// <param name="TcpHeaderProxy">TcpHeader to Delete</param>
        public override void Delete(TcpHeaderProxy TcpHeaderProxy)
        {
            TCPHeader tcpHeader = (from s in DataContext.TCPHeaders where s.Id == TcpHeaderProxy.Id select s).Single();
            DataContext.TCPHeaders.DeleteOnSubmit(tcpHeader);
            DataContext.SubmitChanges();
        }
        #endregion

    }
}
