using gilt.dblinq.proxy;

namespace gilt.dblinq.search
{
    /// <summary>
    /// Search Repository
    /// </summary>
    public interface ISearchRepository : IGenericRepository<SearchProxy>, ISearchEvent
    {        
    }
}
