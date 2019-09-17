using LinqKit;
using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleFilterSpecification : BaseSpecification<Event>
    {
        public ScheduleFilterSpecification(IScheduleFilter filter)
        {
            ApplyCriteria(GetExpression(filter));

            if(filter.DisablePaging != true)
                ApplyPaging(filter.GetSkipValue(), filter.GetTakeValue());

            ApplyOrderByDescending(o => o.StartDate);
        }

        /// <summary>
        /// returns an expression that can be used as a predicate in linq expressions
        /// </summary>
        /// <returns></returns>
        private Expression<Func<Event, bool>> GetExpression(IScheduleFilter filter)
        {
            var predicate = PredicateBuilder.New<Event>();

            predicate = predicate.And(p => p.IsDeleted != true);

            if (DateTime.TryParse(filter.StartDate, out var startDateParsed))
                predicate = predicate.And(p => p.StartDate >= startDateParsed);

            if (DateTime.TryParse(filter.EndDate, out var endDateParsed))
                predicate = predicate.And(p => p.EndDate < endDateParsed);

            return predicate;
        }
    }
}
