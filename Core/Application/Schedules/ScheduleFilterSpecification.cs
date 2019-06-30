using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Specifications;
using UrbanEngine.Core.Common.Filters;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleFilterSpecification : BaseSpecification<Event>
    {
        public ScheduleFilterSpecification(IFilter<Event> filter, IPagingParameters paging)
        {
            ApplyCriteria(filter.GetExpression());
            ApplyPaging(paging);
            ApplyOrderByDescending(o => o.StartDate);
        }
        
    }
}
