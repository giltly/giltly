using gilt.dblinq.proxy;
using gilt.dblinq.search;
using Nancy.Security;
using System.Collections.Generic;

namespace gilt.bacon.modules.search
{
    public partial class SearchModule : ModuleBase<ISearchRepository,SearchProxy>
    {
        private static string EMPTY = " ";
        private static List<string> EMPTY_LIST = new List<string>() { SearchModule.EMPTY };

        public SearchModule(ISearchRepository searchRepository)
            : base(searchRepository, searchRepository)
        {
            this.RequiresAuthentication();            
        }
    }
}
