using gilt.dblinq.events;
using gilt.dblinq.ipdata;
using gilt.dblinq.signatures;
using System;

namespace gilt.unified2.database
{
    public sealed class RepositorySingleton
    {
        private static readonly Lazy<RepositorySingleton> lazy =
            new Lazy<RepositorySingleton>(() => new RepositorySingleton());

        public static RepositorySingleton Instance { get { return lazy.Value; } }

        private RepositorySingleton()
        {
            SignaturesRepo = new SignaturesRepository();
            EventRepo = new EventRepository();
            EventCommentsRepo = new EventCommentsRepository();
            IpHeadersRepo = new IpHeadersRepository();
            IcmpHeadersRepo = new IcmpHeadersRepository();
            DatasRepository = new DatasRepository();
            UdpHeadersRepo = new UdpHeadersRepository();
            TcpHeadersRepository = new TcpHeadersRepository();            
        }

        public SignaturesRepository SignaturesRepo { get; private set; }
        public EventRepository EventRepo { get; private set; }
        public EventCommentsRepository EventCommentsRepo { get; private set; }
        public IpHeadersRepository IpHeadersRepo { get; private set; }
        public IcmpHeadersRepository IcmpHeadersRepo { get; private set; }
        public UdpHeadersRepository UdpHeadersRepo { get; private set; }
        public DatasRepository DatasRepository { get; private set; }
        public TcpHeadersRepository TcpHeadersRepository { get; private set; }
        
    }
}
