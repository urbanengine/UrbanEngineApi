namespace UrbanEngine.Core.Common.Paging
{
    /// <summary>
    /// represents information used to apply pagination to collections and result sets
    /// </summary>
    public interface IPagingParameters
    {
        /// <summary>
        /// page number to start with 
        /// </summary>
        int? PageNumber { get; }

        /// <summary>
        /// number of records per page 
        /// </summary>
        int? PageSize { get; }

        /// <summary>
        /// true to disable paging all together
        /// </summary>
        bool? DisablePaging { get; }

        /// <summary>
        /// returns value to use in call to Take on a linq expression
        /// </summary>
        /// <returns></returns>
        int? GetTakeValue();

        /// <summary>
        /// returns value to use in call to Skip on a linq expression
        /// </summary>
        /// <returns></returns>
        int? GetSkipValue();
    }
}
