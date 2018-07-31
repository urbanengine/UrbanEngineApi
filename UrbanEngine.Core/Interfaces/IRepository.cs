using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UrbanEngine.Core.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(long id) where T : class;
        List<T> List<T>() where T : class;
        T Create<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
    }
}
