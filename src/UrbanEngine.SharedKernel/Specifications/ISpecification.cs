using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UrbanEngine.SharedKernel.Data;

namespace UrbanEngine.SharedKernel.Specifications
{
    public interface ISpecification<TEntity> where TEntity : IEntity
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool EnablePaging { get; }
    }
}
