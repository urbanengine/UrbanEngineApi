using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Messages.Rooms;

namespace UrbanEngine.Web.Controllers.Rooms
{
	/// <summary>
	/// performs any queries relating to Rooms
	/// </summary>
	[Route("odata/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
	public class RoomsController : ODataController
	{
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }
		
        /// <summary>
        /// get Room for specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
		[EnableQuery]
        public async Task<SingleResult<RoomEntity>> GetRoomByIdAsync(long id)
        {
            var result = await _mediator.Send(new QueryRoomMessage { RoomId = id });
            return new SingleResult<RoomEntity>(result);
        }

        /// <summary>
        /// retrieves a list of Rooms based on specified filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
		[EnableQuery]
        public async Task<IQueryable<RoomEntity>> GetRoomsAsync([FromQuery]QueryRoomMessage message)
        {
            var result = await _mediator.Send(message);
            return result;
        }

	}
}
