using Ninja.Api.Common.Models;
using System.Collections.Generic;

namespace Ninja.Api.Services
{
    public interface IUserServiceRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        User UpdateUser(int id, User updatedUser);
    }
}
