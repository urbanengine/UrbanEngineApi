using LinqKit;
using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.Core.Specifications.Venues
{
    public sealed class EventVenueSpecification : BaseSpecification<EventVenueEntity>
    {
        public EventVenueSpecification(IEventVenueFilter filter ) : base( filter )
        {
            ApplyCriteria(GetExpression(filter));
        }

        private Expression<Func<EventVenueEntity, bool>> GetExpression(IEventVenueFilter filter)
        {
            var predicate = PredicateBuilder.New<EventVenueEntity>();

            predicate = filter.IsDeleted.HasValue ? 
                predicate.And(p => p.IsDeleted == filter.IsDeleted.Value) : 
                predicate.And(p => p.IsDeleted != true);

			if(filter.EventVenueId.HasValue)
				predicate = predicate.And(p => p.Id == filter.EventVenueId);

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
