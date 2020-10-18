using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Messages.CheckIn;

namespace UrbanEngine.Web.Controllers.CheckIn
{
	/// <summary>
	/// performs any queries relating to CheckIns
	/// </summary>
	[Route("odata/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CheckInsController : ODataController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CheckInsController(IMediator mediator) {
            _mediator = mediator;
        }

        /// <summary>
        /// get CheckIn for specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[HttpGet("{id}")]
		[EnableQuery]
        public async Task<SingleResult<CheckInEntity>> GetCheckInByIdAsync(long id) {
            var result = await _mediator.Send(new QueryCheckInsMessage { CheckInId = id });
            return new SingleResult<CheckInEntity>(result);
        }

        /// <summary>
        /// retrieves a list of CheckIns based on specified filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
		[HttpGet]
        [EnableQuery]
        public async Task<IQueryable<CheckInEntity>> GetCheckInsAsync([FromQuery]QueryCheckInsMessage message) {
            var result = await _mediator.Send(message);
            return result;
        }
    }
}