using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Managers;

namespace UrbanEngine.Core.Managers.Users
{
	public class UserManager : ManagerBase<UserEntity>, IUserManager
	{
		public UserManager(IAsyncRepository<UserEntity> repository, ILogger<UserManager> logger)
			: base(repository, logger) { }
	}
}
