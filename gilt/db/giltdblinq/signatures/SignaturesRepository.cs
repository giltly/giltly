using gilt.dblinq.proxy;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.signatures
{
    /// <summary>
    /// Signature Repository
    /// </summary>
    public class SignaturesRepository : GenericRepository<SignatureProxy>
    {
        /// <summary>
        /// Initialize the Signature Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from s in DataContext.Signatures select new SignatureProxy(s));           
        }

        #region ISignatureRepository CRUD
        /// <summary>
        /// Update a Signature
        /// </summary>
        /// <param name="SignatureProxy">Signature to Update</param>
        public override void Update(SignatureProxy SignatureProxy)
        {
            Signature existingSignature = (from s in DataContext.Signatures where s.Id == SignatureProxy.Id select s).Single();
            existingSignature.ClassificationId = SignatureProxy.ClassificationId;
            existingSignature.GeneratorId = SignatureProxy.GeneratorId;
            existingSignature.Name = SignatureProxy.Name;
            existingSignature.Priority = SignatureProxy.Priority;
            existingSignature.Revision = SignatureProxy.Revision;
            existingSignature.SignatureIdInternal = SignatureProxy.SignatureIdInternal;              
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a Signature
        /// </summary>
        /// <param name="SignatureProxy">Signature to Add</param>
        public override void Add(SignatureProxy SignatureProxy)
        {
            Signature newSignature = new Signature();
            newSignature.ClassificationId = SignatureProxy.ClassificationId;
            newSignature.GeneratorId = SignatureProxy.GeneratorId;
            newSignature.Name = SignatureProxy.Name;
            newSignature.Priority = SignatureProxy.Priority;
            newSignature.Revision = SignatureProxy.Revision;
            newSignature.SignatureIdInternal = SignatureProxy.SignatureIdInternal;
            DataContext.Signatures.InsertOnSubmit(newSignature);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a Signature
        /// </summary>
        /// <param name="SignatureProxy">Signature to Delete</param>
        public override void Delete(SignatureProxy SignatureProxy)
        {
            Signature sig = (from s in DataContext.Signatures where s.Id == SignatureProxy.Id select s).Single();
            DataContext.Signatures.DeleteOnSubmit(sig);
            DataContext.SubmitChanges();
        }
        #endregion

    }
}
