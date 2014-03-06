using gilt.dblinq.proxy;
using gilt.dblinq.search;
using Nancy;
using System;
using System.Linq;
using System.Net;

namespace gilt.bacon.modules.search
{
    public partial class SearchModule : ModuleBase<ISearchRepository,SearchProxy>
    {
        protected override void JsonResponses()
        {
            Get[GiltlyRoutes.SEARCH_BY_ID] = parameters =>
            {
                decimal id = parameters.Id;
                SearchProxy sp = _repository.FindBy(a => a.Id == id).FirstOrDefault();

                SignatureProxy searchSignature = (null == sp ? null : _repository.GetSignatures().Where(x => x.Id == sp.SignatureId).SingleOrDefault());
                SignatureClassificationProxy searchSignatureClassification = (null == sp ? null : _repository.GetSignatureClassifications().Where(x => x.Id == sp.SignatureClassificationId).SingleOrDefault());

                return Response.AsJson(new
                {
                    SourceIps = _repository.GetDistinctSourceIP().Select(a => new IPAddress(a.ToArray()).ToString()).Concat(SearchModule.EMPTY_LIST),
                    DestinationIps = _repository.GetDistinctSourceIP().Select(a => new IPAddress(a.ToArray()).ToString()).Concat(SearchModule.EMPTY_LIST),
                    SignatureClassifications = _repository.GetSignatureClassifications().Select(a => a.Name).Concat(SearchModule.EMPTY_LIST),
                    Signatures = _repository.GetSignatures().Select(a => a.Name).Concat(SearchModule.EMPTY_LIST),
                    SourcePorts = _repository.GetDistinctSourcePorts(),
                    DestinationPorts = _repository.GetDistinctDestinationPorts(),
                    //SELECTED VALUE from the Id Passed In
                    SelectedId = (sp == null ? 0 : sp.Id),
                    SelectedName = (sp == null ? SearchModule.EMPTY : sp.Name),
                    SelectedSourceIp = (sp == null ? SearchModule.EMPTY : sp.SourceIpString),
                    SelectedDestinationIp = (sp == null ? SearchModule.EMPTY : sp.DestinationIpString),
                    SelectedPacketType = (sp == null ? SearchModule.EMPTY : sp.PacketType),
                    SelectedSignature = (sp == null ? SearchModule.EMPTY : (null == searchSignature ? null : searchSignature.Name)),
                    SelectedSignatureClassification = (sp == null ? SearchModule.EMPTY : (null == searchSignatureClassification ? null : searchSignatureClassification.Name)),
                    SelectedSourcePort = (sp == null ? 0 : sp.SourcePort),
                    SelectedDestinationPort = (sp == null ? 0 : sp.DestinationPort),
                    SelectedStartSearch = (sp == null ? DateTime.Now : sp.StartSearch),
                    SelectedEndSearch = (sp == null ? DateTime.Now : sp.EndSearch),
                });
            };
        }
    }
}
