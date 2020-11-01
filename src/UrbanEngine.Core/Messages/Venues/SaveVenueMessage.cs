using MediatR;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;

namespace UrbanEngine.Core.Messages.Venues
{
    public class SaveVenueMessage : IRequest<EventVenueEntity>
    {
        public ActionType Action { get; set; }
        public EventVenueEntity Detail { get; set; }
    }
}
