namespace UrbanEngine.Core.Common.Paging
{
    public interface IPageableReadOnlyList
    {
        int Skip { get; }

        int Take { get; }

        int TotalCount { get; }

        IPagingResult GetPagingResult();
    }
}
