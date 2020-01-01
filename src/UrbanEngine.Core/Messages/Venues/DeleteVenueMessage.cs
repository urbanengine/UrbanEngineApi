using MediatR;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Venues
{
    public class DeleteVenueMessage : IRequest<CommandResult>
    {
        public long Id { get; set; }
    }
}
