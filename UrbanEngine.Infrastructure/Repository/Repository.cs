namespace UrbanEngine.Infrastructure.Repository {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class Repository : IRepository {
        #region Local Members

        private readonly DbContext _dbContext = null;

        #endregion

        #region Constructors

        public Repository( DbContext dbContext ) {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods
 
        public async Task<TEntity> SingleOrDefaultAsync<TEntity>( Expression<Func<TEntity, bool>> predicate = null ) where TEntity : class {
            var result = await Get( predicate ).SingleOrDefaultAsync();
            return result; 
        }
         
        public async Task<TEntity> FirstOrDefaultAsync<TEntity>( Expression<Func<TEntity, bool>> predicate = null ) where TEntity : class {
            var result = await Get( predicate ).FirstOrDefaultAsync();
            return result;
        }
         
        public async Task<IEnumerable<TEntity>> ListAsync<TEntity>( Expression<Func<TEntity, bool>> predicate = null ) where TEntity : class {
            var result = await Get( predicate ).ToListAsync();
            return result;
        }

        public async Task<TEntity> GetByIdAsync<TEntity>( long id ) where TEntity : class {
            return await FindAsync<TEntity>( id ); 
        }

        public async Task<TEntity> FindAsync<TEntity>( params object[] keyValues ) where TEntity : class {
            var result = await _dbContext.FindAsync<TEntity>( keyValues );
            return result;
        }

        public IQueryable<TEntity> Get<TEntity>( Expression<Func<TEntity, bool>> predicate = null ) where TEntity : class {
            return predicate != null ? Set<TEntity>().Where( predicate ) : Set<TEntity>();
        }
         
        public async Task<int> CreateAsync<TEntity>( params TEntity[] entities ) where TEntity : class {
            // add all 
            _dbContext.AddRange( entities );

            // persist 
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync<TEntity>( params TEntity[] entities ) where TEntity : class {
            // update all 
            _dbContext.UpdateRange( entities );

            // persist 
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync<TEntity>( params TEntity[] entities ) where TEntity : class {
            // remove all  
            _dbContext.RemoveRange( entities );

            // persist 
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public IQueryable<TEntity> Set<TEntity>() where TEntity : class {
            return _dbContext.Set<TEntity>();
        }

        #endregion
    }
}
