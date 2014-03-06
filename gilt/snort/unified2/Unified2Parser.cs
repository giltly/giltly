using System;
using System.Diagnostics;
using System.IO;
using NLog;
using MiscUtil.IO;
using MiscUtil.Conversion;
using System.Threading.Tasks.Dataflow;
using gilt.unified2.database;

namespace gilt.unified2.packets
{
    /// <summary>
    /// Process the Log File using Microsoft.Tpl.Dataflow 
    /// The general flow:
    /// 1. Get the packet data
    /// 2. Parse the packet data into PacketBase -- take byte[] of length record_type and turn it into an object
    /// 3. Store the packet to the database
    /// </summary>
    public class Unified2Parser :  IDisposable
    {        
        private FileStream _fileStream = null;
        private EndianBinaryReader _binaryReader = null;
        private BigEndianBitConverter _endianConvertor = null;
        private static Logger _giltUnified2Logger = LogManager.GetLogger("giltunified2");

        /// <summary>
        /// Create a unfied2 file parser
        /// </summary>
        /// <param name="FileName">The unfied2 log file path</param>
        /// <param name="ByteOffset">The offset in bytes to start parsing packets</param>
        public Unified2Parser(string FileName, ulong ByteOffset)
        {
            //TODO:  do we need to lock this class out from the logging controller events?
            _endianConvertor = new BigEndianBitConverter();
            _fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _fileStream.Position = (long)ByteOffset;
            _binaryReader = new EndianBinaryReader(_endianConvertor, _fileStream);
        }

        /// <summary>
        /// Parse each packet in order
        /// </summary>
        public void Parse()
        {            
            while (_binaryReader.BaseStream.Position != _binaryReader.BaseStream.Length)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var readPacketHeader = this.ReadPacketHeader();
                var parsePacketData = this.ParsePacketData();

                //chain the packet output depending on configuration                
                var storePacketDatabase = this.StorePacketDatabase();
                var finalizePacket = this.FinalizePacket();

                readPacketHeader.LinkTo(parsePacketData);
                parsePacketData.LinkTo(storePacketDatabase);
                storePacketDatabase.LinkTo(finalizePacket);                

                readPacketHeader.Completion.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        ((IDataflowBlock)parsePacketData).Fault(t.Exception);
                    }
                    else
                    {
                        parsePacketData.Complete();
                    }
                });
                parsePacketData.Completion.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        ((IDataflowBlock)storePacketDatabase).Fault(t.Exception);
                    }
                    else
                    {
                        storePacketDatabase.Complete();
                    }
                });
                storePacketDatabase.Completion.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        ((IDataflowBlock)finalizePacket).Fault(t.Exception);
                    }
                    else
                    {
                        finalizePacket.Complete();
                    }
                });

                // start processing the log file
                readPacketHeader.Post(_binaryReader);

                // Mark the head of the pipeline as complete. The continuation tasks  
                // propagate completion through the pipeline as each part of the pipeline finishes.  
                readPacketHeader.Complete();

                // Wait for the last block in the pipeline to process all messages.
                finalizePacket.Completion.Wait();

                //TODO: modulous printing of percent complete
                _giltUnified2Logger.Info(String.Format("{0} ms    Percent Done: {1}", sw.ElapsedMilliseconds, ((double)_fileStream.Position / (double)_fileStream.Length).ToString("P")  ));
            }
        }

        /// <summary>
        /// Get the offset in bytes of the binary reader
        /// </summary>
        /// <returns>Offset in number of bytes</returns>
        public long GetByteOffset()
        {
            return _fileStream.Position;
        }

        /// <summary>
        /// Dispose of the binaryreader
        /// </summary>
        public void Dispose()
        {
            _binaryReader.Dispose();
            _fileStream.Dispose();
        }

        /// <summary>
        /// Read in the Packet Header
        /// </summary>
        /// <returns></returns>
        private TransformBlock<EndianBinaryReader, Tuple<EndianBinaryReader, Unified2Header>> ReadPacketHeader()
        {
            return new TransformBlock<EndianBinaryReader, Tuple<EndianBinaryReader, Unified2Header>>(reader =>
            {
                //get the header information
                uint recordType = _binaryReader.ReadUInt32();
                uint recordLength = _binaryReader.ReadUInt32();
                return new Tuple<EndianBinaryReader, Unified2Header>(reader, new Unified2Header(recordType, recordLength));
            });
        }

        /// <summary>
        /// Parse the bytes into a packet base class
        /// </summary>
        /// <returns></returns>
        private TransformBlock<Tuple<EndianBinaryReader, Unified2Header>, PacketBase> ParsePacketData()
        {
            return new TransformBlock<Tuple<EndianBinaryReader, Unified2Header>, PacketBase>(pack =>
            {
                Unified2Factory pf = new Unified2Factory(pack.Item1, pack.Item2);
                return pf.CreatePacketParser(pack.Item2.RecordType);
            });
        }

        /// <summary>
        /// Store Packet to Database
        /// </summary>
        /// <returns></returns>
        private TransformBlock<PacketBase, PacketBase> StorePacketDatabase()
        {
            return new TransformBlock<PacketBase, PacketBase>(pack =>
            {
                PacketDatabaseFactory.InsertDbPacket(pack);
                return pack;
            });
        }

        /// <summary>
        /// Place Holder for a No Operation
        /// </summary>
        /// <returns></returns>
        private ActionBlock<PacketBase> FinalizePacket()
        {            
            return new ActionBlock<PacketBase>(pack =>
            {
            });         
        }
    }
}
