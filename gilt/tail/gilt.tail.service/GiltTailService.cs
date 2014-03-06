using System.ServiceProcess;
using System.Security.Permissions;
using System.Threading;
using gilt.tail;

namespace gilt.tail.service
{
    /// <summary>
    /// Gilt Tail Windows Service
    /// </summary>
    public partial class GiltTailService : ServiceBase
    {
        private const string EVENT_LOG_SOURCE = "GiltTailSource";
        private const string EVENT_LOG_LOG = "GiltTailLog";
        private ServiceEntry _giltService = null;
        private Thread _giltServiceThread = null;

        /// <summary>
        /// Create a new tail service and attach EVENT_LOG_SOURCE as a windows service event log
        /// </summary>
        public GiltTailService()
        {
            try
            {
                InitializeComponent();
                // Initialize GiltTailLog   
                if (!System.Diagnostics.EventLog.SourceExists(EVENT_LOG_SOURCE))
                {
                    System.Diagnostics.EventLog.CreateEventSource(EVENT_LOG_SOURCE, EVENT_LOG_LOG);
                }
                giltTailEventLog.Source = EVENT_LOG_SOURCE;
                giltTailEventLog.Log = EVENT_LOG_LOG;
                _giltService = new ServiceEntry();
                _giltServiceThread = new Thread(new ThreadStart(_giltService.Start));

            }
            catch (System.Exception e)
            {
                giltTailEventLog.WriteEntry(e.ToString());                
            }            
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void KillIt()
        {
            _giltServiceThread.Abort();
        }

        #region ServiceBase
        /// <summary>
        /// Start the Thread upon starting the service
        /// </summary>
        /// <param name="args">Start arguments</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                giltTailEventLog.WriteEntry("Gilt Tail Service Starting");
                _giltServiceThread.Start();                
                giltTailEventLog.WriteEntry("Gilt Tail Service Started");
            }
            catch (System.Exception e)
            {
                giltTailEventLog.WriteEntry(e.ToString());                                
            }
        }

        /// <summary>
        /// Kill the tailing service thread when the service is stopped
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                giltTailEventLog.WriteEntry("Gilt Tail Service Stopping");
                KillIt();
                giltTailEventLog.WriteEntry("Gilt Tail Service Stopped");
            }
            catch (System.Exception e)
            {
                giltTailEventLog.WriteEntry(e.ToString());                                
            }
        }
        #endregion 
    }
}
