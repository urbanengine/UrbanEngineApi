using System.Collections.Generic;

namespace UrbanEngine.Infrastructure.Repository
{
    public interface IRepository
    {
        T GetById<T>(long id) where T : class;
        List<T> List<T>() where T : class;
        void Create<T>(T entity, bool autoSave) where T : class;
        void Update<T>(T entity, bool autoSave) where T : class;
        void Delete<T>(long id, bool autoSave) where T : class;
        void Delete<T>(T entity, bool autoSave) where T : class;
    }
}