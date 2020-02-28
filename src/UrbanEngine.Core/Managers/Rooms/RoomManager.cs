using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Managers;

namespace UrbanEngine.Core.Managers.Rooms
{
	public class RoomManager : ManagerBase<RoomEntity>, IRoomManager
	{
		public RoomManager(IAsyncRepository<RoomEntity> repository, ILogger<ManagerBase<RoomEntity>> logger) 
			: base(repository, logger) { }
	}
}
