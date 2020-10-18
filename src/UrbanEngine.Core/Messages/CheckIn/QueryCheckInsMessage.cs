using System;
using System.Linq;
using MediatR;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Specifications.CheckIns;

namespace UrbanEngine.Core.Messages.CheckIn
{
	public sealed class QueryCheckInsMessage : IRequest<IQueryable<CheckInEntity>>, ICheckInFilter
	{
		public long? CheckInId { get; set; }
		public long? EventId { get; set; }
		public long? UserId { get; set; }
		public DateTimeOffset? CheckedInAt { get; set; }
		public bool? IsDeleted { get; set; }
	}
}
