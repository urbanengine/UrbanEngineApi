using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UrbanEngine.Core.Application.Specifications;

namespace UrbanEngine.Infrastructure.Persistence.Data
{
    internal sealed class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            
            return query;
        }

        public static IQueryable<TProjected> GetProjectedQuery<TProjected>(IQueryable<TEntity> inputQuery, IProjectedSpecification<TEntity, TProjected> specification)
        {
            if (!(specification?.IsProjected ?? false))
                throw new InvalidOperationException("in order to use GetProjectedQuery a Selector must be specified for the IProjectedSpecification");

            // apply base specification 
            var query = GetQuery(inputQuery, specification);

            // project using the specified selector
            IQueryable<TProjected> projectedQuery = query.Select(specification.Selector);
            return projectedQuery; 
        }
    }
}
