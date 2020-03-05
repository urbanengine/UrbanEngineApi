using System;
using System.Linq.Expressions;
using LinqKit;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.Core.Specifications.Events
{
	/// <summary>
	/// find an event for a specified room and within a specified date range
	/// </summary>
	public class EventByRoomSpecification : BaseSpecification<EventEntity>
	{
		public EventByRoomSpecification(long roomId, DateTimeOffset startDateTime, DateTimeOffset endDateTime)
		{
			ApplyCriteria(GetExpression(roomId, startDateTime, endDateTime));
		}

		private Expression<Func<EventEntity, bool>> GetExpression(long roomId, DateTimeOffset startDateTime, DateTimeOffset endDateTime)
		{
            var predicate = PredicateBuilder.New<EventEntity>();

			predicate = predicate.And(p => p.IsDeleted != true);
			predicate = predicate.And(p => p.StartDate >= startDateTime);
			predicate = predicate.And(p => p.EndDate <= endDateTime);
			predicate = predicate.And(p => p.RoomId == roomId);

			return predicate;
		}
	}
}
