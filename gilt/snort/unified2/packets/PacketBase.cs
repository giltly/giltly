using System;
using System.Reflection;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using MiscUtil.IO;

namespace gilt.unified2.packets
{
    /// <summary>
    /// Base class that holds the endian aware binary reader and the Unified2 header
    /// </summary>
    [DataContract(Name = "PacketBase", Namespace = "http://www.gilty.com")]
    public abstract class PacketBase 
    {
        /// <summary>
        /// ToString() padding for print
        /// </summary>
        private const int TOSTRING_LEFT_PAD = -30;
        /// <summary>
        /// Endian aware binary reader attached to the packet data
        /// </summary>
        protected EndianBinaryReader _binaryReader = null;
        /// <summary>
        /// The unified2 header defining the length and type of packet
        /// </summary>
        protected Unified2Header _header = null;
        /// <summary>
        /// Unified2 header holding the length and type of packet
        /// </summary>
        public Unified2Header UnifiedHeader
        {
            get { return _header; }
        }

        /// <summary>
        /// Store the endian reader and unified2header
        /// </summary>
        /// <param name="EndianReader"></param>
        /// <param name="Unified2Header"></param>
        public PacketBase(EndianBinaryReader EndianReader, Unified2Header Unified2Header)
        {            
            _binaryReader = EndianReader;
            _header = Unified2Header;
        }
        
        /// <summary>
        /// Return a printable version of the Packet
        /// </summary>
        /// <returns>printable version of string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (FieldInfo member in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                try
                {
                    if (member.MemberType == MemberTypes.Field)
                    {
                        string name = member.Name;
                        object val = member.GetValue(this);
                        string lineFormat = String.Format("{{0,{0}}}: {1}", TOSTRING_LEFT_PAD, val.ToString());
                        sb.AppendLine(String.Format(lineFormat, name));
                    }
                }
                catch
                { }
            }
            return sb.ToString();
        }
    }
}
