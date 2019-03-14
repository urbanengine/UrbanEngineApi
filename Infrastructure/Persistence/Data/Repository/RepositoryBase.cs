using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanEngine.Infrastructure.Persistence.Data.Repository
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
    }
}
