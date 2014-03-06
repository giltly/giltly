using gilt.dblinq.proxy;
using gilt.dblinq.search;

namespace gilt.bacon.modules.search
{
    public partial class SearchModule : ModuleBase<ISearchRepository,SearchProxy>
    {
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.SEARCH_ADD_VIEW] = parameters =>
            {
                return this.RenderView("AddSearch");
            };

            Get[GiltlyRoutes.SEARCH_EDIT_VIEW] = parameters =>
            {
                return this.RenderView("EditSearch");
            };
        }
    }
}
