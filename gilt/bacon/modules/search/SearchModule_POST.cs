using gilt.dblinq.proxy;
using gilt.dblinq.search;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Linq;

namespace gilt.bacon.modules.search
{
    public partial class SearchModule : ModuleBase<ISearchRepository,SearchProxy>
    {
        protected override void PostResponses()
        {
            Post[GiltlyRoutes.SEARCH_DELETE_ID] = parameters =>
            {
                decimal id = parameters.Id;
                SearchProxy sp = _repository.FindBy(a => a.Id == id).FirstOrDefault();
                return _crudDelegate(sp, CrudEnum.Delete);
            };

            Post[GiltlyRoutes.SEARCH_UPDATE] = parameters =>
            {
                //grab the user id -- for creation of new items
                this.GetModel();

                SearchProxy sp = this.Bind<SearchProxy>();

                //the user selects the signature by name but the database is storing the FK                
                if (Request.Form.ContainsKey("Signature"))
                {
                    string sigName = Request.Form["Signature"];
                    if (null != sigName)
                    {
                        SignatureProxy sig = _repository.GetSignatures().Where(x => x.Name == sigName).FirstOrDefault();
                        if (null != sig)
                        {
                            sp.SignatureId = sig.Id;
                        }
                    }
                }

                //the user selects the signature classification by name but the database is storing the FK                
                if (Request.Form.ContainsKey("SignatureClassification"))
                {
                    string sigClassName = Request.Form["SignatureClassification"];
                    if (null != sigClassName)
                    {
                        SignatureClassificationProxy sigc = _repository.GetSignatureClassifications().Where(x => x.Name == sigClassName).FirstOrDefault();
                        if (null != sigc)
                        {
                            sp.SignatureClassificationId = sigc.Id;
                        }
                    }
                }

                //
                // the nancy binding from the form  this.Bind<SearchProxy>
                // inserts empty byte arrays into SourceIp and DestinationIP
                // this is not a valid IP address when trying to save to the database -- NULL is
                //
                if (sp.SourceIp == new byte[] {})
                {
                    sp.SourceIp = null;
                }
                if (sp.DestinationIp == new byte[] { })
                {
                    sp.DestinationIp = null;
                }

                if (sp.Id > 0)
                {
                    return _crudDelegate(sp, CrudEnum.Update);
                }
                else
                {
                    sp.CreatedBy = _baseModel.UserModel.UserProxy.Id;
                    sp.CreatedOn = DateTime.Now;

                    return _crudDelegate(sp, CrudEnum.Add);
                }
            };
        }
    }
}
