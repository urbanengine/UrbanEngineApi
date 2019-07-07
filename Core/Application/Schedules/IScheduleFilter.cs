using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Schedules
{
    /// <summary>
    /// used to filter and paginate schedule results
    /// </summary>
    public interface IScheduleFilter : IPagingParameters
    {
        /// <summary>
        /// start date
        /// </summary>
        string StartDate { get; }

        /// <summary>
        /// end date
        /// </summary>
        string EndDate { get; }
    }
}
