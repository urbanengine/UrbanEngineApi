using MediatR;
using System.Collections.Generic;
using UrbanEngine.Core.Models.Rooms;
using UrbanEngine.Core.Specifications.Rooms;
using UrbanEngine.SharedKernel.Paging;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Rooms
{
	public class GetRoomsMessage : PagingParameters, IRequest<QueryResult<IEnumerable<RoomListItemDto>>>, IRoomFilter
	{
		public string Name { get; set; }

		public int? MinCapacity { get; set; }

		public long? VenueId { get; set; }

		public bool? HasTvOrProjector { get; set; }

		public string Resources { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
