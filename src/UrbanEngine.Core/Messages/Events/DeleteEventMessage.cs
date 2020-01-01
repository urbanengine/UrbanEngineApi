using MediatR;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Events
{
    public class DeleteEventMessage : IRequest<CommandResult>
    {
        public long Id { get; set; }
    }
}
