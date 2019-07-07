using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;

namespace UrbanEngine.Tests.Persistence.Tests.TestHelpers.Scopes
{
    public abstract class RepositoryTestScope<TRepository> : TestScope<TRepository>, IDisposable
        where TRepository : class, IRepository
    {
        private UrbanEngineTestDbContext _context;
        private SqliteConnection _connection;

        public RepositoryTestScope() : base()
        {
        }

        public void Dispose()
        {
            _context?.Dispose();
            _connection?.Close();
        }

        internal virtual UrbanEngineTestDbContext GetDbContext()
        {
            if(_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();
            }

            if(_context == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<UrbanEngineTestDbContext>();
                optionsBuilder.UseSqlite(_connection);

                _context = new UrbanEngineTestDbContext(optionsBuilder.Options);
                _context.Database.EnsureCreated();  
            }
            return _context;
        }
    }

}
