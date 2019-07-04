using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Common.Results
{
    public class QueryResult : ResultBase
    { 
        public object Data { get; private set; }

        public IPagingResult Paging { get; private set; }
        
        public override string ResultType => "Query";

        public QueryResult(object data, IPagingResult paging = null)
        {
            Data = data; 
            Paging = paging;
        }
        
    }

    public class QueryResult<T> : QueryResult
    {
        public QueryResult(T data, IPagingResult paging = null)
            : base(data, paging) { }

        public static QueryResult<T> New(T data)
        {
            IPagingResult paging;
            if(data is IPageableReadOnlyList) 
                paging = ((IPageableReadOnlyList)data).GetPagingResult(); 
            else 
                paging = PagingResult.None; 

            return new QueryResult<T>(data, paging);
        }
    }
}
