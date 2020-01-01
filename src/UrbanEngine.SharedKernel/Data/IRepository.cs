using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.SharedKernel.Specifications;

namespace UrbanEngine.SharedKernel.Data
{
    public interface IRepository { }

    public interface IAsyncRepository<TEntity> : IRepository where TEntity : IEntity
    {
        Task<TEntity> GetByIdAsync(object id); 
        Task<TEntity> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<IReadOnlyList<TEntity>> ListAllAsync();

        Task<int> CountAsync(ISpecification<TEntity> specification);
        Task<bool> AnyAsync(ISpecification<TEntity> specification);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification);
        Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification);
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification);
    }
}
