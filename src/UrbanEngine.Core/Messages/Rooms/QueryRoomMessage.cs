using System.Linq;
using MediatR;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Specifications.Rooms;

namespace UrbanEngine.Core.Messages.Rooms
{
	public sealed class QueryRoomMessage : IRequest<IQueryable<RoomEntity>>, IRoomFilter
	{
		public long? RoomId { get; set; }

		public string Name { get; set; }

		public int? MinCapacity { get; set; }

		public long? VenueId { get; set; }

		public bool? HasTvOrProjector { get; set; }

		public string Resources { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
