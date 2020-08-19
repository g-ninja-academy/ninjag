using Ninja.Application.Common.Models;
using System.Collections.Generic;

namespace Ninja.Application.Services
{
    public interface IUserServiceRepository
    {
        IEnumerable<UserVm> GetUsers();
        UserVm GetUserById(int id);
        UserVm CreateUser(UserVm user);
        UserVm UpdateUser(int id, UserVm updatedUser);
    }
}
