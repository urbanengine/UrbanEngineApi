using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanEngine.Infrastructure.Data.Extensions;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.Infrastructure.Data.Repository
{
    public abstract class EfRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;

        protected EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).SingleOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<bool> AnyAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).AnyAsync();
        }
         
        public async Task<int> CountAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification)
        {
            var queryable = ApplySpecification(specification);

            var result = specification.EnablePaging ? 
                await queryable.ToPagedListAsync(specification.Skip, specification.Take) : 
                await queryable.ToListAsync();
            
            return result;
        }

		public IQueryable<TEntity> Query(ISpecification<TEntity> specification)
		{
			var queryable = ApplySpecification(specification);
			return queryable;
		}

        public async Task<TEntity> CreateAsync(TEntity entity) 
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
         
        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
         
        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
