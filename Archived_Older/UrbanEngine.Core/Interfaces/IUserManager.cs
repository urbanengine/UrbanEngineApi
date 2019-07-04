﻿using System.Collections.Generic;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Core.Interfaces
{
    public interface IUserManager
    {
        User GetUserById(long id);
        IEnumerable<User> ListUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(long id);
    }
}