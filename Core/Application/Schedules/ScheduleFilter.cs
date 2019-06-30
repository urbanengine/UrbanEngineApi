using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Filters;

namespace UrbanEngine.Core.Application.Schedules
{
    /// <summary>
    /// used to filter schedule results
    /// </summary>
    public class ScheduleFilter : IFilter<Event>
    {
        /// <summary>
        /// start date
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// end date
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// returns an expression that can be used as a predicate in linq expressions
        /// </summary>
        /// <returns></returns>
        public Expression<Func<Event, bool>> GetExpression()
        {
            return p => 1 == 1;
        }
    }
}
