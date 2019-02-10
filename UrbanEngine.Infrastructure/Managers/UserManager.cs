using System;
using System.Collections.Generic;
using UrbanEngine.Core;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Interfaces;
using UrbanEngine.Infrastructure.Repository;

namespace UrbanEngine.Infrastructure.Managers {
    public class UserManager : IUserManager {
        #region Fields

        private IDbRepository _repository = null;

        #endregion

        #region Constructor

        public UserManager( IDbRepository repository ) {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public User GetUserById( long id ) {
            if( id <= 0 )
                throw new ArgumentException( "id must be greater than 0" );

            return null;// return _repository.GetById<User>( id );
        }

        public IEnumerable<User> ListUsers() {
            return null;// return _repository.List<User>();
        }

        public void AddUser( User user ) {
            //_repository.Create( user, true );
        }

        public void UpdateUser( User user ) {
            //_repository.Update( user, true );
        }

        public void DeleteUser( long id ) {
            //_repository.Delete<User>( id, true );
        }

        #endregion
    }
}
