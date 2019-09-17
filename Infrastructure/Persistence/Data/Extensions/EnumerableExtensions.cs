using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Infrastructure.Persistence.Data.Extensions
{
    public static class EnumerableExtensions
    {
        public static async Task<IReadOnlyList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int skip, int take)
        {
            var totalCount = queryable.Count();

            var data = await queryable
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var pagedResult = new PageableReadOnlyList<T>(data, skip, take, totalCount);
            return pagedResult;
        }
    }
}
