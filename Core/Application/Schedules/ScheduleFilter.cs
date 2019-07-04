using System;
using System.Linq.Expressions;
using LinqKit;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Filters;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Schedules
{
    /// <summary>
    /// used to filter and paginate schedule results
    /// </summary>
    public class ScheduleFilter : PagingParameters, IFilter<Event>, IPagingParameters
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
            var predicate = PredicateBuilder.New<Event>();

            predicate = predicate.And(p => p.IsDeleted != true);

            if (DateTime.TryParse(StartDate, out var startDateParsed))
                predicate = predicate.And(p => p.StartDate >= startDateParsed);

            if (DateTime.TryParse(EndDate, out var endDateParsed))
                predicate = predicate.And(p => p.EndDate < endDateParsed);

            return predicate;
        }
    }
}
