namespace UrbanEngine.Web.V1
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using MediatR;
	using Microsoft.AspNet.OData;
	using Microsoft.AspNet.OData.Routing;
	using Microsoft.AspNetCore.Mvc;
	using UrbanEngine.Core.Entities;
	using UrbanEngine.Core.Enums;
	using UrbanEngine.Core.Messages.Venues;
	using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
    using static Microsoft.AspNetCore.Http.StatusCodes;

	/// <summary>
	/// Represents a RESTful service of Event Venues
	/// </summary>
	[ApiVersion("1.0")]
	[ODataRoutePrefix("EventVenues")]
	public class EventVenuesController : ODataController
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="mediator"></param>
		public EventVenuesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Gets a single event venue for specified id
		/// </summary>
		/// <param name="id">The requested event venue identifier</param>
		/// <returns>The requested event venue</returns>
		[ODataRoute( "{id}" )]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( EventVenueEntity ), Status200OK )]
        [ProducesResponseType( Status404NotFound )]
        [EnableQuery( AllowedQueryOptions = Select )]
		public async Task<SingleResult<EventVenueEntity>> Get(long id)
		{
            var result = await _mediator.Send(new QueryVenuesMessage { EventVenueId = id });
            return SingleResult.Create(result);
		}

		/// <summary>
		/// Gets a collection of event venues that meet that specified criteria
		/// </summary>
		/// <returns>event venues that match the request</returns>
		[HttpGet]
		[ODataRoute]
        [Produces( "application/json" )]
		[ProducesResponseType( typeof( ODataValue<IEnumerable<EventVenueEntity>> ), Status200OK)]
        [EnableQuery( MaxTop = 100, AllowedQueryOptions = Select | Filter | Top | Skip | Count | OrderBy )]
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new QueryVenuesMessage());
			return Ok(result);
		}

		/// <summary>
		/// Creates a new event venue
		/// </summary>
		/// <param name="entity">parameters used to generate the new venue</param>
		/// <returns></returns>
		[ODataRoute]
		[Produces("application/json")]
		[ProducesResponseType( typeof( EventVenueEntity ), Status201Created )]
        [ProducesResponseType( Status400BadRequest )]
		public async Task<IActionResult> Post( [FromBody] EventVenueEntity entity )
		{
			var result = await _mediator.Send(new SaveVenueMessage {
				Detail = entity,
				Action = ActionType.Create
			});
			return Created(result);
		}

		/// <summary>
		/// Creates a new event venue
		/// </summary>
		/// <returns></returns>
		[ODataRoute( "{id}" )]
		[Produces("application/json")]
		[ProducesResponseType( typeof( EventVenueEntity ), Status200OK )]
        [ProducesResponseType( Status204NoContent )]
        [ProducesResponseType( Status400BadRequest )]
        [ProducesResponseType( Status404NotFound )]
		public async Task<IActionResult> Patch( long id, Delta<EventVenueEntity> delta )
		{
            var patchedEntity = (await _mediator.Send(new QueryVenuesMessage { EventVenueId = id }))
				.SingleOrDefault();

			if( patchedEntity == null )
				return NotFound();

			delta.Patch( patchedEntity );

			var result = await _mediator.Send(new SaveVenueMessage {
				Detail = patchedEntity,
				Action = ActionType.Update
			});

			return Updated( result );
		}

		/// <summary>
		/// Performs a soft delete of an existing event venue
		/// </summary>
		/// <param name="id">id of venue to mark as deleted</param>
		/// <returns></returns>
		[ODataRoute( "{id}" )]
		[ProducesResponseType( Status204NoContent )]
        [ProducesResponseType( Status404NotFound )]
		public async Task<IActionResult> Delete( long id )
		{
			await _mediator.Send(new DeleteVenueMessage
			{
				Id = id
			});

			return NoContent();
		}
		
		/// <summary>
		/// retrieve all rooms for a secified evenue
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[ODataRoute( "{id}/Rooms" )]
		[Produces( "application/json" )]
		[ProducesResponseType( typeof(ODataValue<IEnumerable<RoomEntity>>), Status200OK )]
		[ProducesResponseType( Status404NotFound )]
		[EnableQuery( AllowedQueryOptions = Select )]
		public async Task<IActionResult> GetRooms( long id )
		{
            var result = await _mediator.Send(new QueryVenuesMessage { EventVenueId = id });
            var rooms = result.Select(s => s.Rooms);
			return Ok(rooms);
		}
	}
}
