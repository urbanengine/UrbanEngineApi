using Microsoft.EntityFrameworkCore;

namespace UrbanEngine.Infrastructure.Repository
{
    public interface IDbRepository : IRepository { }

    public class DbRepository : Repository, IDbRepository
    {
        #region Constructor

        public DbRepository(DbContext dbContext)
            : base(dbContext) { }

        #endregion
    }
}
