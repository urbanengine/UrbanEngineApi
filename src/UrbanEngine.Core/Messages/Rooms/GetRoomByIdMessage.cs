using MediatR;
using UrbanEngine.Core.Models.Rooms;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Rooms
{
	public class GetRoomByIdMessage: IRequest<QueryResult<RoomDetailDto>>
    {
        public long Id { get; set; }
	}
}
