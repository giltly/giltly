
namespace gilt.snort
{
    /// <summary>
    /// Describe how write a snort configuration
    /// </summary>
    public interface IGenericConfigWriter
    {        
        /// <summary>
        /// Write a snort configuration to the database
        /// </summary>
        void WriteToDatabase();
    }
}
