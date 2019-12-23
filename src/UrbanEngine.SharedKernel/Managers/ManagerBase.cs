using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.SharedKernel.Managers
{
    public abstract class ManagerBase<TEntity> : IManager<TEntity> where TEntity : IEntity
    {
        private readonly IAsyncRepository<TEntity> _repository;
        private readonly ILogger _logger;

        public ManagerBase(IAsyncRepository<TEntity> repository, ILogger<ManagerBase<TEntity>> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public async Task<TEntity> GetByIdAsync(object id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification)
        {
            var result = await _repository.ListAsync(specification);
            return result;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await _repository.CreateAsync(entity);
            return result;
        }
        
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await _repository.UpdateAsync(entity);
            if(result <= 0)
            {
                throw new Exception("failed to save");
            }
            return await GetByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            var result = await _repository.DeleteAsync(entity);
            var isDeleted = result > 0;
            return isDeleted;
        }
    }
}
