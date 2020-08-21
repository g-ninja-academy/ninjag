using Ninja.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ninja.Application.Services
{
    public class UsersService : IUsersService
    {
        private static readonly List<UserVm> _users = new List<UserVm>();

        public UserVm CreateUser(UserVm user)
        {
            _users.Add(user);

            return user;
        }

        public UserVm GetUserById(int id)
        {
            return _users.SingleOrDefault(user => user.Id == id);
        }

        public IEnumerable<UserVm> GetUsers()
        {
            return _users;
        }

        public UserVm UpdateUser(int id, UserVm updatedUser)
        {
            var user = _users.FirstOrDefault(m => m.Id == id);

            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
            }

            return user;
        }
    }
}
