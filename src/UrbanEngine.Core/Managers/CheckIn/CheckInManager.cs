using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Managers;

namespace UrbanEngine.Core.Managers.CheckIn {
    public class CheckInManager : ManagerBase<CheckInEntity>, ICheckInManager {
        public CheckInManager(IAsyncRepository<CheckInEntity> repository, ILogger<ManagerBase<CheckInEntity>> logger)
            : base(repository, logger) { }
    }
}
