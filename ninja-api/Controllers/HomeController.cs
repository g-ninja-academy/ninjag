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
        private static readonly List<User> _users = new List<User>();
        
        // GET: api/<HomeController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _users;
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _users.FirstOrDefault(m => m.Id == id);
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            var newUsr = new User(user.Id, user.Name, user.Email);
            _users.Add(newUsr);
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            var usr = _users.FirstOrDefault(m => m.Id == id);
            if (usr == null)
            {
                Console.WriteLine($"No User with Id {id} were found");
            }
            else
            {
                usr.Name = user.Name;
                usr.Email = user.Email;
            }
        }
    }
}