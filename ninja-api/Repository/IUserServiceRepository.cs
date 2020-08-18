using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ninja_api.Repository
{
    public interface IUserServiceRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        User UpdateUser(int id, User updatedUser);
    }
}
