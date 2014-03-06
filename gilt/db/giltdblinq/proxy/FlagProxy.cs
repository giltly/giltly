
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Flag proxy
    /// <see href="http://manual.snort.org/node463.html"/>
    /// </summary>
    public sealed class FlagProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Number
        /// </summary>
        public byte Number { get; set; }
        /// <summary>
        /// Reserverd 1
        /// </summary>
        public int? RES1 { get; set; }
        /// <summary>
        /// Reserved 2
        /// </summary>
        public int? RES2 { get; set; }
        /// <summary>
        /// Urget
        /// </summary>
        public int? URG { get; set; }
        /// <summary>
        /// Acknowledgment
        /// </summary>
        public int? ACK { get; set; }
        /// <summary>
        /// Push 
        /// </summary>
        public int? PSH { get; set; }
        /// <summary>
        /// Reset 
        /// </summary>
        public int? RST { get; set; }
        /// <summary>
        /// Synchronize sequence numbers 
        /// </summary>
        public int? SYN { get; set; }
        /// <summary>
        /// Finish (LSB in TCP Flags byte) 
        /// </summary>
        public int? FIN { get; set; }
        /// <summary>
        /// Vaild
        /// </summary>
        public int? Valid { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Create a flag proxy from a flag
        /// </summary>
        /// <param name="F">The flag</param>
        public FlagProxy(Flag F)
            : base()
        {
            if (null != F)
            {
                Id = F.Id;
                Number = F.Number;
                RES1 = F.RES1;
                RES2 = F.RES2;
                URG = F.URG;
                ACK = F.ACK;
                PSH = F.PSH;
                RST = F.RST;
                SYN = F.SYN;
                FIN = F.FIN;
                Valid = F.Valid;
                Description = F.Description;
            }
        }
    }
}
