using System;
using System.Collections.Generic;
using UrbanEngine.Core;
using UrbanEngine.Core.Interfaces;
using UrbanEngine.Infrastructure.Repository;

namespace UrbanEngine.Infrastructure.Managers
{
    public class UserManager : IUserManager
    {
        #region Fields

        private IDbRepository _repository = null;

        #endregion

        #region Constructor

        public UserManager(IDbRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public User GetUser(long id)
        {
            if (id <= 0)
                throw new ArgumentException("id must be greater than 0");

            return _repository.GetById<User>(id);
        }

        public IEnumerable<User> ListUsers()
        {
            return _repository.List<User>();
        }

        #endregion
    }
}
