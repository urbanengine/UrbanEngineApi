using Microsoft.EntityFrameworkCore;
using UrbanEngine.Infrastructure.Context;

namespace UrbanEngine.Infrastructure.Repository
{
    public interface IDbRepository : IRepository { }

    public class DbRepository : Repository, IDbRepository
    {
        #region Constructor

        public DbRepository(UrbanEngineContext dbContext)
            : base(dbContext) { }

        #endregion
    }
}
