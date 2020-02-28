using System;
using System.Linq.Expressions;
using LinqKit;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.Core.Specifications.Rooms
{
	public class RoomSpecification : BaseSpecification<RoomEntity>
	{
		public RoomSpecification(IRoomFilter filter) : base(filter)
		{
			ApplyCriteria(GetExpression(filter));
		}

		private Expression<Func<RoomEntity, bool>> GetExpression(IRoomFilter filter)
		{
			var predicate = PredicateBuilder.New<RoomEntity>();
			
            predicate = filter.IsDeleted.HasValue ?
                predicate.And(p => p.IsDeleted == filter.IsDeleted.Value) :
                predicate.And(p => p.IsDeleted != true);

			if(filter.MinCapacity.HasValue)
				predicate = predicate.And(p => p.Capacity >= filter.MinCapacity);

			if(filter.HasTvOrProjector.HasValue)
				predicate = predicate.And(p => p.HasTVOrProjector == filter.HasTvOrProjector);

			if(!string.IsNullOrWhiteSpace(filter.Name))
				predicate = predicate.And(p => p.Name.ToLower().Contains(filter.Name.ToLower()));

			if(!string.IsNullOrEmpty(filter.Resources))
				predicate = predicate.And(p => p.Resources.ToLower().Contains(filter.Resources.ToLower()));

			if(filter.VenueId.HasValue)
				predicate = predicate.And(p => p.VenueId == filter.VenueId);

			return predicate;
		}
	}
}
