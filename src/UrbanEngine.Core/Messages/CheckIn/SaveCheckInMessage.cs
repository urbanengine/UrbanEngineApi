using MediatR;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.CheckIn {
    public class SaveCheckInMessage : IRequest<CommandResultWithData>
    {
        public ActionType Action { get; set; }
        public CheckInDetailDto Detail { get; set; }
    }
}
