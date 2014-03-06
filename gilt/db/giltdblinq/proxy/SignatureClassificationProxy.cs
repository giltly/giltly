
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// SignatureClassification Proxy
    /// </summary>
    public sealed class SignatureClassificationProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public decimal Id { get; set; }
        /// <summary>
        /// Classification Id
        /// </summary>
        public decimal ClassificationId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Default Priority
        /// </summary>
        public byte? DefaultPriority { get; set; }

        /// <summary>
        /// Create a SignatureClassificationProxy from a SignatureClassification
        /// </summary>
        /// <param name="Sc">The SignatureClassification</param>
        public SignatureClassificationProxy(SignatureClassification Sc)        
        {
            if (null != Sc)
            {
                Id = Sc.Id;
                ClassificationId = Sc.ClassificationId;                
                Name = Sc.Name;
                Description = Sc.Description;
                DefaultPriority = Sc.DefaultPriority;
            }
        }
    }
}
