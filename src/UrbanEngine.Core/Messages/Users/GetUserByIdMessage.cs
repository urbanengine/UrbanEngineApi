using MediatR;
using UrbanEngine.Core.Models.Users;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Users
{
	public class GetUserByIdMessage : IRequest<QueryResult<UserDetailDto>>
	{
		public long Id { get; set; }
	}
}
