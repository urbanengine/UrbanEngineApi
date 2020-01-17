using MediatR;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.CheckIn {
    public class DeleteCheckInMessage : IRequest<CommandResult>
    {
        public long Id { get; set; }
    }
}
