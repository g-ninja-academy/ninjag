using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ninja_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly FakeUserService _fakeService = new FakeUserService();

        // GET: api/<HomeController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var getUsers = _fakeService.GetFakeUsersService();
            return getUsers;
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var getUsers = _fakeService.GetFakeUsersService();
            return getUsers.FirstOrDefault(m => m.Id == id);
        }

        // POST api/<HomeController>
        [HttpPost]
        public User Post([FromBody] User user)
        {
            var newUsr = new User(user.Id, user.Name, user.Email);
            var getUsers = _fakeService.GetFakeUsersService().ToList();

            getUsers.Add(newUsr);

            return getUsers.LastOrDefault();
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User user)
        {
            var getUsers = _fakeService.GetFakeUsersService().ToList();

            var updateUser = getUsers.FirstOrDefault(m => m.Id == id);
            if (updateUser == null)
            {
                Console.WriteLine($"No User with Id {id} were found");
            }
            else
            {
                updateUser.Name = user.Name;
                updateUser.Email = user.Email;
            }

            return updateUser;
        }
    }
}