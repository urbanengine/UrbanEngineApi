using MediatR;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Users
{
	public class DeleteUserMessage : IRequest<CommandResult>
	{
		public long Id { get; set; }
	}
}
