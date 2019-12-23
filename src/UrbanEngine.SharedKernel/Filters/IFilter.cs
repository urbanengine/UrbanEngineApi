using System;
using System.Linq.Expressions;

namespace UrbanEngine.SharedKernel.Filters
{
    /// <summary>
    /// represents an object used to apply a filter
    /// </summary>
    public interface IFilter<T> where T : class
    {
        /// <summary>
        /// returns an expression that can be used in Linq predicates
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Expression<Func<T, bool>> GetExpression();
    }
}
