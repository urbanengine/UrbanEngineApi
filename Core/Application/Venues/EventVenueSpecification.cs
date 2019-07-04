using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Core.Application.Venues
{
    public class EventVenueSpecification : BaseSpecification<EventVenue>
    {
        public EventVenueSpecification(EventVenueFilter filter)
        {
            ApplyCriteria(filter.GetExpression()); 

            if(filter.DisablePaging != true)
                ApplyPaging(filter.GetSkipValue(), filter.GetTakeValue());
        }
    }
}
