
namespace gilt.unified2.enums
{
    /// <summary>
    /// Type specifies the type of extra data that was logged, the valid types are: 
    /// </summary>
    public enum ExtraDataTypes
    {
        /// <summary>
        /// Original Client IPv4. Blob size = 4
        /// </summary>
        OriginalClientIPV4 = 1, /*  */
        /// <summary>
        /// Original Client IPv6. Blob size = 16
        /// </summary>
        OriginalClientIPV6 = 2, 
        /// <summary>
        /// Unused
        /// </summary>
        Unused = 3,
        /// <summary>
        /// GZIP Decompressed Data
        /// </summary>
        GZipDecompressedData = 4,
        /// <summary>
        /// SMTP Filename
        /// </summary>
        SMTPFileName = 5,
        /// <summary>
        /// SMTP Mail From
        /// </summary>
        SMTPMailFrom = 6,
        /// <summary>
        /// SMTP RCPT To
        /// </summary>
        SMTPRCPTTo = 7,
        /// <summary>
        /// SMTP Email Headers
        /// </summary>
        SMTPEmailHeaders = 8,
        /// <summary>
        /// HTTP URI
        /// </summary>
        HTTPUri = 9,
        /// <summary>
        /// HTTP Hostname. blob size less than 256
        /// </summary>
        HTTPHostName = 10, 
        /// <summary>
        /// IPv6 Source Address. blob size = 16
        /// </summary>
        IPv6SourceAddress = 11,  
        /// <summary>
        /// IPv6 Destniation Address. blob size = 16
        /// </summary>
        IPv6DestinationAddress = 12,  
        /// <summary>
        /// Normalize Javascript data <see href="http://blog.snort.org/2012/01/snort-2920-javascript-normalization.html" />
        /// </summary>
        NormalizedJavascriptData = 13,  
        /// <summary>
        /// Unknown extra data type
        /// </summary>
        Unknown
    }
}
