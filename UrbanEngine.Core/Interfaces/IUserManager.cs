using System.Collections.Generic;

namespace UrbanEngine.Core.Interfaces
{
    public interface IUserManager
    {
        User GetUser(long id);
        IEnumerable<User> ListUsers();
    }
}
