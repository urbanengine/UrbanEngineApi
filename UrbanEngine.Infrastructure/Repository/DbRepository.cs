using Microsoft.EntityFrameworkCore;
using UrbanEngine.Infrastructure.Context;

namespace UrbanEngine.Infrastructure.Repository
{
    public interface IDbRepository : IRepository { }

    public class DbRepository : Repository, IDbRepository
    {
        #region Constructor

        public DbRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        #endregion
    }
}
