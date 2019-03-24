using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrbanEngine.Core.Application.Interfaces.Persistence.Data
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification);
        Task<int> CountAsync(ISpecification<TEntity> specification);
        Task<bool> AnyAsync(ISpecification<TEntity> specification);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification);
        Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
