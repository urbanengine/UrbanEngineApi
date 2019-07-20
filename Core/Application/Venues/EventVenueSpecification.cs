using LinqKit;
using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Core.Application.Venues
{
    public class EventVenueSpecification : ProjectedBaseSpecification<EventVenue, IEventVenueModel>
    {
        public EventVenueSpecification(IEventVenueFilter filter, IEventVenueModel selector)
        {
            ApplyCriteria(GetExpression(filter)); 

            if(filter.DisablePaging != true)
                ApplyPaging(filter.GetSkipValue(), filter.GetTakeValue());

            ApplySelector(selector.Projection);
        }

        public EventVenueSpecification(Expression<Func<EventVenue, bool>> criteria, IEventVenueModel selector = null)
        {
            ApplyCriteria(criteria);

            if(selector != null)
                ApplySelector(selector.Projection);
        }

        private Expression<Func<EventVenue, bool>> GetExpression(IEventVenueFilter filter)
        {
            var predicate = PredicateBuilder.New<EventVenue>();

            predicate = filter.IsDeleted.HasValue ? 
                predicate.And(p => p.IsDeleted == filter.IsDeleted.Value) : 
                predicate.And(p => p.IsDeleted != true);

            if (!string.IsNullOrEmpty(filter.City))
                predicate = predicate.And(p => p.City.ToLower().Trim() == filter.City.ToLower().Trim());

            if (!string.IsNullOrEmpty(filter.State))
                predicate = predicate.And(p => p.State.ToLower().Trim() == filter.State.ToLower().Trim());

            if (!string.IsNullOrEmpty(filter.PostalCode))
                predicate = predicate.And(p => p.PostalCode.ToLower().Trim() == filter.PostalCode.ToLower().Trim());

            if (!string.IsNullOrEmpty(filter.Region) && RegionType.TryFromName(filter.Region, out var regionTypeFromName))
                predicate = predicate.And(p => p.Region == regionTypeFromName);
            else if (!string.IsNullOrEmpty(filter.Region) && int.TryParse(filter.Region, out var regionValue) && RegionType.TryFromValue(regionValue, out var regionTypeFromValue))
                predicate = predicate.And(p => p.Region == regionTypeFromValue);

            return predicate;
        }
    }
    
}
