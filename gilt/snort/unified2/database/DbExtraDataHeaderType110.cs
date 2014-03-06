using System.Transactions;
using gilt.dblinq;
using gilt.unified2.packets;
using gilt.util;

namespace gilt.unified2.database
{
    public sealed class DbExtraDataHeaderType110 : DbPacketGenericBase<ExtraDataHeaderType110>
    {
        public DbExtraDataHeaderType110(PacketBase pb)
            : base((ExtraDataHeaderType110)pb)
        {
        }

        public override void ToDatabase()
        {
            Data d = new Data();
            d.SensorId = _packet.SensorId;
            d.EventId = _packet.EventId;
            //All of these Extra data types, with the exception of 1, 2, 11, and 12 (IP Addresses) 
            //are stored in plain-text. 
            //The IP Address types need to be interpreted as if they were comming off the wire
            d.Payload = System.Text.Encoding.ASCII.GetString(_packet.Data);

            _datasRepository.Add(new dblinq.proxy.DataProxy(d));
        }
    }
}
