using MediatR;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.Users;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Users
{
	public class SaveUserMessage : IRequest<CommandResultWithData>
	{
		public ActionType Action { get; set; }
		public UserDetailDto Detail { get; set; }
	}
}
