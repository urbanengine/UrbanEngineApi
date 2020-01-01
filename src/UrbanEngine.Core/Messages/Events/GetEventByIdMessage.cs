using MediatR;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Events
{
    public class GetEventByIdMessage : IRequest<QueryResult<EventDetailDto>>
    {
        public long Id { get; set; }
    }
}
