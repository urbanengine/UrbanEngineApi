using MediatR;
using UrbanEngine.Core.Models.Venues;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Venues 
{
    public class GetVenueByIdMessage : IRequest<QueryResult<EventVenueDetailDto>>
    {
        public long Id { get; set; }
    }
}
