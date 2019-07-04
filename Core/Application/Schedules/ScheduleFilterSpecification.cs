using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleFilterSpecification : BaseSpecification<Event>
    {
        public ScheduleFilterSpecification(ScheduleFilter filter)
        {
            ApplyCriteria(filter.GetExpression());

            if(filter.DisablePaging != true)
                ApplyPaging(filter.GetSkipValue(), filter.GetTakeValue());

            ApplyOrderByDescending(o => o.StartDate);
        }
        
    }
}
