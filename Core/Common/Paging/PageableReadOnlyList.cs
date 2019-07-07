using System.Collections.Generic;
namespace UrbanEngine.Core.Common.Paging
{
    public class PageableReadOnlyList<T> : List<T>, IPageableReadOnlyList
    {
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public int TotalCount { get; private set; }
        
        public PageableReadOnlyList(IEnumerable<T> pagedData, int skip, int take, int totalCount) 
            : base(pagedData)
        {
            TotalCount = totalCount;
            Skip = skip;
            Take = take;
        }

        public IPagingResult GetPagingResult()
        {
            return new PagingResult(Skip, Take, TotalCount); 
        }
    }
}
