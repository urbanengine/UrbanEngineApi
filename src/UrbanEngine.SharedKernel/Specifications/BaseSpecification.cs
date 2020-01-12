using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Paging;

namespace UrbanEngine.SharedKernel.Specifications
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : IEntity
    {

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected BaseSpecification( IPagingParameters pagingParameters )
        {
            if ( pagingParameters.DisablePaging != true )
                ApplyPaging( pagingParameters.GetSkipValue(), pagingParameters.GetTakeValue() );
        }

        protected BaseSpecification() { }

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool EnablePaging { get; private set; }

        protected virtual void ApplyCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            EnablePaging = true;
        }
        
        protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
    }

}
