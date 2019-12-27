using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.SharedKernel.Managers
{
    public interface IManager
    {
    }

    public interface IManager<TEntity> : IManager where TEntity : IEntity
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(object id, bool softDelete);
    }
}
