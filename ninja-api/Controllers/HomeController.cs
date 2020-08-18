using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ninja_api.Repository;

namespace ninja_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public readonly IUserServiceRepository _userServiceRespository;

        public HomeController(IUserServiceRepository userServiceRespository)
        {
            _userServiceRespository = userServiceRespository;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userServiceRespository.GetUsers();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userServiceRespository.GetUserById(id);
        }

        [HttpPost]
        public User Post([FromBody] User user)
        {
            return _userServiceRespository.CreateUser(user);
        }

        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User user)
        {
            return _userServiceRespository.UpdateUser(id, user);
        }
    }
}