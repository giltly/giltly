
namespace gilt.dblinq.proxy
{
    /// <summary>
    /// Signature Proxy
    /// </summary>
    public sealed class SignatureProxy : ProxyBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public decimal Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Classification Id
        /// </summary>
        public decimal ClassificationId { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        public decimal? Priority { get; set; }
        /// <summary>
        /// Revision
        /// </summary>
        public decimal? Revision { get; set; }
        /// <summary>
        /// Signature Id Internal
        /// </summary>
        public decimal? SignatureIdInternal { get; set; }
        /// <summary>
        /// Generator Id - GID
        /// </summary>
        public decimal? GeneratorId { get; set; }

        /// <summary>
        /// Create a SignatureProxy from a Signature
        /// </summary>
        /// <param name="S">The Signature</param>
        public SignatureProxy(Signature S)            
        {
            if (null != S)
            {
                Id = S.Id;
                Name = S.Name;
                ClassificationId = S.ClassificationId;
                Priority = S.Priority;
                Revision = S.Revision;
                SignatureIdInternal = S.SignatureIdInternal;
                GeneratorId = S.GeneratorId;
            }
        }
    }
}
