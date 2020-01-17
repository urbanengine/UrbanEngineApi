using MediatR;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.CheckIn {
    public class GetCheckInByIdMessage : IRequest<QueryResult<CheckInDetailDto>>
    {
        public long Id { get; set; }
    }
}
