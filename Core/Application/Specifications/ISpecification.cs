using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UrbanEngine.Core.Application.Specifications
{
    public interface ISpecification<TEntity>
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

    public interface IProjectedSpecification<TEntity, TResult> : ISpecification<TEntity>
    {
        Expression<Func<TEntity, TResult>> Selector { get; }
        bool IsProjected { get; }
    }
}
