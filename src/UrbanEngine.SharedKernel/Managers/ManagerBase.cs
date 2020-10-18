using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.SharedKernel.Managers
{
    public abstract class ManagerBase<TEntity> : IManager<TEntity> where TEntity : IEntity
    {
        protected readonly IAsyncRepository<TEntity> Repository;
        private readonly ILogger _logger;

        public ManagerBase(IAsyncRepository<TEntity> repository, ILogger<ManagerBase<TEntity>> logger)
        {
            Repository = repository;
            _logger = logger;
        }
        
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            if(id == null)
                throw new ArgumentNullException(nameof(id));

            var result = await Repository.GetByIdAsync(id);
            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification)
        {
            var result = await Repository.ListAsync(specification);
            return result;
        }

        public virtual IQueryable<TEntity> Query(ISpecification<TEntity> specification)
        {
            var result = Repository.Query(specification);
            return result;
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null)
        {
            var result = Repository.Query(predicate);
            return result;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await Repository.CreateAsync(entity);
            return result;
        }
        
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await Repository.UpdateAsync(entity);
            if(result <= 0)
            {
                throw new Exception("failed to save");
            }
            return await GetByIdAsync(entity.Id);
        }

        public virtual async Task<bool> DeleteAsync(object id, bool softDelete)
        {
            var entity = await GetByIdAsync(id);

            int result;
            if(!softDelete)
            {
                result = await Repository.DeleteAsync(entity);           
            }
            else
            {
                entity.IsDeleted = true;
                result = await Repository.UpdateAsync(entity);
            }
            return result > 0;
        }
    }
}
