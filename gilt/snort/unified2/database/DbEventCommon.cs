using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.snort;
using gilt.unified2.packets;
using System;
using System.Linq;

namespace gilt.unified2.database
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DbEventCommon<T> : DbPacketGenericBase<T> where T : EventCommon
    {
        public DbEventCommon(T pb)
            : base(pb)
        {
        }

        protected decimal InsertNewEvent()
        {
            SignatureProxy sp = _signaturesRepo.FindBy(s => (s.SignatureIdInternal == _packet.SignatureId)
                                     && (s.ClassificationId == _packet.ClassificationId)
                                     && (s.Revision == _packet.SignatureRevision)
                                     && (s.GeneratorId == _packet.GeneratorId)
                                     && (s.Priority == _packet.PriorityId)).ToList().FirstOrDefault();

            //The signature does not exist in the database
            if (null == sp)
            {
                string sigName = "unknown";
                if (SnortMsgMapConfig.SnortIdToSignatureName.ContainsKey(_packet.SignatureId))
                {
                    sigName = SnortMsgMapConfig.SnortIdToSignatureName[_packet.SignatureId];
                }
                _signaturesRepo.Add(new dblinq.proxy.SignatureProxy(new Signature()
                {
                    Name = sigName,
                    ClassificationId = _packet.ClassificationId,
                    Priority = _packet.PriorityId,
                    Revision = _packet.SignatureRevision,
                    SignatureIdInternal = _packet.SignatureId,
                    GeneratorId = _packet.GeneratorId
                }));
            }

            sp = _signaturesRepo.FindBy(s => (s.SignatureIdInternal == _packet.SignatureId)
                                     && (s.ClassificationId == _packet.ClassificationId)
                                     && (s.Revision == _packet.SignatureRevision)
                                     && (s.GeneratorId == _packet.GeneratorId)
                                     && (s.Priority == _packet.PriorityId)).ToList().FirstOrDefault();
            
            if (null != _eventRepo.GetEventsByPK((int)_packet.SensorId, (int)_packet.EventId))
            {
                this.DeleteLastFailedEvent((decimal)_packet.EventId, _packet.SensorId);
            }

            _eventRepo.Add(new dblinq.proxy.EventProxy(new Event()
            {
                EventId = (decimal)_packet.EventId,
                SensorId = _packet.SensorId,
                TimeStamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_packet.EventSecond).AddMilliseconds(1000 * _packet.EventMiroSecond),
                SignatureId = sp.Id
            }));
            return _eventRepo.GetEventsByPK((int)_packet.SensorId, (int)_packet.EventId).EventId;
        }

        /// <summary>
        /// Delete all information when an event fails to parse
        /// This is needed when the service is stopped or crashes for some reason and leaves items floating
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="sensorId"></param>
        private void DeleteLastFailedEvent(decimal eventId, uint sensorId)
        {
            foreach (EventCommentsProxy ep in _eventCommentsRepo.FindBy(x => x.SensorId == sensorId && x.EventId == eventId))
            {
                _eventCommentsRepo.Delete(ep);
            }
            foreach (DataProxy dp in _datasRepository.FindBy(x => x.SensorId == sensorId && x.EventId == eventId))
            {
                _datasRepository.Delete(dp);
            }
            foreach (IpHeaderProxy ip in _ipHeadersRepo.FindBy(x => x.SensorId == sensorId && x.EventId == eventId))
            {
                _ipHeadersRepo.Delete(ip);
            }
            foreach (IcmpHeaderProxy icmp in _icmpHeadersRepo.FindBy(x => x.SensorId == sensorId && x.EventId == eventId))
            {
                _icmpHeadersRepo.Delete(icmp);
            }
            foreach (TcpHeaderProxy tp in _tcpHeadersRepository.FindBy(x => x.SensorId == sensorId && x.EventId == eventId))
            {
                _tcpHeadersRepository.Delete(tp);
            }
            foreach (UdpHeaderProxy up in _udpHeadersRepo.FindBy(x => x.SensorId == sensorId && x.EventId == eventId))
            {
                _udpHeadersRepo.Delete(up);
            }
            foreach (EventProxy ep in _eventRepo.FindBy(x => x.SensorId == sensorId && x.EventId == eventId))
            {
                _eventRepo.Delete(ep);
            }
        }
    }
}
