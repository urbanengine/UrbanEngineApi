using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UrbanEngine.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        #region Fields

        private DbContext _dbContext = null;

        #endregion

        #region Constructors

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public T GetById<T>(long id) where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        public List<T> List<T>() where T : class
        {
            return _dbContext.Set<T>().ToList();
        }

        public void Create<T>(T entity, bool autoSave = false) where T : class
        {
            _dbContext.Set<T>().Add(entity);

            if (autoSave)
                SaveChanges();
        }

        public void Update<T>(T entity, bool autoSave = false) where T : class
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            if (autoSave)
                SaveChanges();
        }

        public void Delete<T>(long id, bool autoSave = false) where T : class
        {
            T entity = GetById<T>(id);
            if (entity == null)
                throw new Exception(string.Concat("unable to find item with id ", id));

            Delete(entity, autoSave);

        }

        public void Delete<T>(T entity, bool autoSave = false) where T : class
        {
            var dbSet = _dbContext.Set<T>();
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);

            if (autoSave)
                SaveChanges();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        #endregion
    }
}
