namespace UrbanEngine.SharedKernel.Paging
{
    public class PagingParameters : IPagingParameters
    {
        public const int DefaultMaxPageSize = 50;

        #region Constructors

        public PagingParameters() { }

        public PagingParameters(int? pageNumber, int? pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            DisablePaging = !pageSize.HasValue || pageSize <= 0;
        }

        #endregion

        #region IPagingParameters Members

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public bool? DisablePaging { get; set; }

        public int GetSkipValue()
        {
            return (PageNumber ?? 1) - 1;
        }

        public int GetTakeValue()
        {
            return PageSize ?? DefaultMaxPageSize;
        }

        #endregion

        #region Static Methods

        public static IPagingParameters Empty => new PagingParameters();

        public static IPagingParameters Default => new PagingParameters(1, 50);

        public static IPagingParameters Disabled => new PagingParameters { DisablePaging = true };

        #endregion
    }
}
