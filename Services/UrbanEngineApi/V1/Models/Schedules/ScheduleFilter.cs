using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Schedules;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Models.Schedules
{
    /// <summary>
    /// used to filter and paginate schedule results
    /// </summary>
    public class ScheduleFilter : PagingParameters, IScheduleFilter
    {
        #region IScheduleFilter Members 

        /// <summary>
        /// start date
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// end date
        /// </summary>
        public string EndDate { get; set; }

        #endregion
    }
}
