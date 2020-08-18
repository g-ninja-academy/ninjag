using Ninja.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ninja.Application.Services
{
    public class UserService : IUserServiceRepository
    {
        private static readonly List<User> _users = new List<User>();

        public User CreateUser(User user)
        {
            _users.Add(user);

            return user;
        }

        public User GetUserById(int id)
        {
            return _users.SingleOrDefault(user => user.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        public User UpdateUser(int id, User updatedUser)
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
