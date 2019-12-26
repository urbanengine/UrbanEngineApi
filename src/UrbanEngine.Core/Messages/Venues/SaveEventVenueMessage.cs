using MediatR;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.Venues;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Venues
{
    public class SaveEventVenueMessage : IRequest<CommandResultWithData>
    {
        public ActionType Action { get; set; }
        public EventVenueDetailDto Detail { get; set; }
    }
}
