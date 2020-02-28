using MediatR;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Rooms
{
	public class DeleteRoomMessage : IRequest<CommandResult>
	{
		public long Id { get; set; }
	}
}
