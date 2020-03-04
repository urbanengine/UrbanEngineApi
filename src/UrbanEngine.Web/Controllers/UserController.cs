using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Messages.Events;
using UrbanEngine.Core.Messages.Users;
using UrbanEngine.Core.Models.Users;

namespace UrbanEngine.Web.Controllers
{
	/// <summary>
	/// Returns information about users
	/// </summary>
	[Route("api/[controller")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// Constructor for the UserController
		/// </summary>
		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET user/
		/// <summary>
		/// retrieve a user by their id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetUserById(long id)
		{
			var result = await _mediator.Send( new GetUserByIdMessage { Id = id });
			return Ok(result);
		}

		/// <summary>
		/// retrieves a list of users based on specified filter
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetUsersAsync([FromQuery]GetUsersMessage message)
		{
			var result = await _mediator.Send(message);
			return Ok(result);
		}

		/// <summary>
		/// add a new user to the system
		/// </summary>
		/// <param name="data"></param>
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateUserAsync([FromQuery]UserDetailDto data)
		{
			var message = new SaveUserMessage
			{
				Detail = data,
				Action = ActionType.Create
			};
			var result = await _mediator.Send(message);
			return Ok(result);
		}

		/// <summary>
		/// update an existing user in the system
		/// </summary>
		/// <param name="data"></param>
		[HttpPut]
		[Authorize]
		public async Task<IActionResult> UpdateUserAsync([FromQuery]UserDetailDto data)
		{
			var message = new SaveUserMessage
			{
				Detail = data,
				Action = ActionType.Update
			};
			var result = await _mediator.Send(message);
			return Ok(result);
		}

		// DELETE user/5
		/// <summary>
		/// remove a user from the system by id
		/// </summary>
		/// <param name="message"></param>
		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> DeleteUserAsync([FromQuery]DeleteEventMessage message)
		{
			var result = await _mediator.Send(message);
			return Ok(result);
		}
	}
}
