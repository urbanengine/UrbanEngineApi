using MediatR;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.Rooms;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Rooms
{
	public class SaveRoomMessage: IRequest<CommandResultWithData>
    {
        public ActionType Action { get; set; }
        public RoomDetailDto Detail { get; set; }
	}
}
