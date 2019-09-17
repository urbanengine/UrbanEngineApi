using LinqKit;
using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Core.Application.Events
{
    public sealed class EventSpecification : ProjectedBaseSpecification<Event, IEventModel>
    {
        public EventSpecification(IEventFilter filter, IEventModel selector)
        {
            ApplyCriteria(GetExpression(filter)); 

            if(filter.DisablePaging != true)
                ApplyPaging(filter.GetSkipValue(), filter.GetTakeValue());

            ApplySelector(selector.Projection);
        }
         
        public EventSpecification(Expression<Func<Event, bool>> criteria, IEventModel selector = null)
        {
            ApplyCriteria(criteria);

            if(selector != null)
                ApplySelector(selector.Projection);
        }

        private Expression<Func<Event, bool>> GetExpression(IEventFilter filter)
        {
            var predicate = PredicateBuilder.New<Event>();

            predicate = filter.IsDeleted.HasValue ? 
                predicate.And(p => p.IsDeleted == filter.IsDeleted.Value) : 
                predicate.And(p => p.IsDeleted != true);

            if (filter.StartDate.HasValue)
                predicate = predicate.And(p => p.StartDate >= filter.StartDate);

            if (filter.EndDate.HasValue)
                predicate = predicate.And(p => p.EndDate <= filter.EndDate);

            if (filter.VenueId.HasValue)
                predicate = predicate.And(p => p.VenueId == filter.VenueId);

            if (!string.IsNullOrEmpty(filter.OrganizerId))
                predicate = predicate.And(p => p.OrganizerId == filter.OrganizerId);

            if (filter.EventType != null)
                predicate = predicate.And(p => p.EventType == filter.EventType);

            return predicate;
        }
    }
}
