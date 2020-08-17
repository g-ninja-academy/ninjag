using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ninja_api
{
    public class FakeUserService
    {
        private static readonly List<User> _users = new List<User>();
        public FakeUserService()
        {
            
        }
        public IEnumerable<User> GetFakeUsersService()
        {
            _users.Add(new User(1, "Roberto", "roberto@globant.com"));
            _users.Add(new User(2, "Emmanuel", "emmanuel@globant.com"));

            return _users;
        }
    }
}
