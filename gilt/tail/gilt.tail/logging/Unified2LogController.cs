using gilt.dblinq;
using gilt.dblinq.logs;
using gilt.dblinq.proxy;
using gilt.unified2.packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace gilt.tail.logging
{
    /// <summary>
    /// Consumes the output of Unified2LogWatcher file system events and hands them over to a Unified2Parser 
    /// </summary>
    public class Unified2LogController
    {                
        private Unified2LogWatcher _unified2LogWatcher = null;
        private LogHistoryRepository _logHistoryRepository = null;
        private LogEntryRepository _logEntryRepository = null;

        /// <summary>
        /// Create a new Unified2 Log Controller and subscribe to changes from the Unified2LogWatcher
        /// </summary>
        /// <param name="LogWatcher">The unified2 log file watcher attached to the snort log defined in app.config</param>
        public Unified2LogController(Unified2LogWatcher LogWatcher)
        {
            //start watching the filesystem
            _unified2LogWatcher = LogWatcher;
            _unified2LogWatcher.LogFileEvent += _snortLogWatcher_LogFileEvent;
            _logEntryRepository = new LogEntryRepository();
            _logHistoryRepository = new LogHistoryRepository();
        }
        
        /// <summary>
        /// Process the file system events from the snort filesystem log watcher
        /// TODO:  does this need to be wrapped in an atomic?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void _snortLogWatcher_LogFileEvent(object sender, FileSystemEventArgs args)
        {
            List<LogHistoryProxy> lh = _logHistoryRepository.GetAll().ToList();
            if (0 == lh.Count())
            {
                const int BYTE_OFFSET_START = 0;
                FileInfo fi = new FileInfo(args.FullPath);

                //no master log entry exists add the entry
                LogEntry logEnt = new LogEntry();
                logEnt.CreatedOn = fi.CreationTime;
                logEnt.ModifiedOn = fi.LastWriteTime;
                logEnt.SizeBytes = (int)fi.Length;
                logEnt.FileName = fi.FullName;
                logEnt.StartedOn = DateTime.Now;

                //get the next packet and process all of the data                
                Unified2Parser pp = new Unified2Parser(fi.FullName, BYTE_OFFSET_START);
                pp.Parse();

                //create the master log history pointer
                logEnt.FinishedOn = DateTime.Now;

                LogHistory logHist = new LogHistory();
                logHist.CurrentOffsetBytes = (int)pp.GetByteOffset();                
                _logEntryRepository.Add(new LogEntryProxy(logEnt));

                //set the PK from the row that was just written
                logHist.CurrentLogEntryId = _logEntryRepository.FindBy(x => x.StartedOn == logEnt.StartedOn && x.FinishedOn == logEnt.FinishedOn).Single().Id;

                _logHistoryRepository.Add(new LogHistoryProxy(logHist));                
            }
            else
            {
                //TODO: the program has already read some stuff but events are firing
            }
        }
    }
}