using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Core.Application.Interfaces.Persistence.Data
{
    public interface IRepository { }

    public interface IAsyncRepository<TEntity> : IRepository where TEntity : class
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

        Task<TProjected> FirstOrDefaultAsync<TProjected>(IProjectedSpecification<TEntity, TProjected> specification);
        Task<TProjected> SingleOrDefaultAsync<TProjected>(IProjectedSpecification<TEntity, TProjected> specification);
        Task<IReadOnlyList<TProjected>> ListAsync<TProjected>(IProjectedSpecification<TEntity, TProjected> specification);
    }
}
