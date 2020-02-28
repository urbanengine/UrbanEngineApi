using LinqKit;
using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.Core.Specifications.Events
{
    public sealed class EventSpecification : BaseSpecification<EventEntity>
    {
        public EventSpecification(IEventFilter filter) : base( filter )
        {
            ApplyCriteria(GetExpression(filter));
        }

        private Expression<Func<EventEntity, bool>> GetExpression(IEventFilter filter)
        {
            var predicate = PredicateBuilder.New<EventEntity>();

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

			if(filter.RoomId.HasValue)
				predicate = predicate.And(p => p.RoomId == filter.RoomId);

            return predicate;
        }
    }
}
