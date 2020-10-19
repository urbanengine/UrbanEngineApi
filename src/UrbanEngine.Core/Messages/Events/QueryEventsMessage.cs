using System;
using System.Linq;
using MediatR;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Specifications.Events;

namespace UrbanEngine.Core.Messages.Events
{
	public sealed class QueryEventsMessage : IRequest<IQueryable<EventEntity>>, IEventFilter
	{
		public long? EventId { get; set; }

		public DateTimeOffset? StartDate { get; set; }

		public DateTimeOffset? EndDate { get; set; }

		public EventType EventType { get; set; }

		public string OrganizerId { get; set; }

		public bool? IsDeleted { get; set; }

		public long? RoomId { get; set; }
	}
}
