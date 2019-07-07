namespace UrbanEngine.Core.Common.Paging
{
    public interface IPagingResult
    {
        int TotalCount { get; }
        int PageSize { get; }
        int CurrentPage { get; }
        int TotalPages { get; }
        bool PreviousPage { get; }
        bool NextPage { get; }
        bool IsPaged { get; }
    }
}
