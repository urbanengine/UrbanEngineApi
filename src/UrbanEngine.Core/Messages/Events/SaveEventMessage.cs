using MediatR;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Events
{
    public class SaveEventMessage : IRequest<CommandResultWithData>
    {
        public ActionType Action { get; set; }
        public EventDetailDto Detail { get; set; }
    }
}
