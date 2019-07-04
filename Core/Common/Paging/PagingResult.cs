using System;

namespace UrbanEngine.Core.Common.Paging
{
    public class PagingResult : IPagingResult
    {
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public bool PreviousPage { get; private set; }
        public bool NextPage { get; private set; }
        public bool IsPaged { get; private set; }

        public PagingResult(int skip, int take, int totalCount, bool disablePaging = false)
        {
            if (disablePaging)
            {
                CurrentPage = 1;
                PageSize = totalCount;
                TotalCount = totalCount;
                TotalPages = 1;
                PreviousPage = false;
                NextPage = false;
                IsPaged = false;
            }
            else
            {
                CurrentPage = skip + 1;
                PageSize = take;
                TotalCount = totalCount;
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
                PreviousPage = CurrentPage > 1;
                NextPage = CurrentPage < TotalPages;
                IsPaged = true;
            }
        }

        private PagingResult() { }

        public static IPagingResult None => new PagingResult
        {
            IsPaged = false,
            PreviousPage = false,
            NextPage = false
        };
    }
}
