using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;

namespace Ninja.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserServiceRepository _userServiceRespository;

        public UserController(IUserServiceRepository userServiceRespository)
        {
            _userServiceRespository = userServiceRespository;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return Ok(_userServiceRespository.GetUsers());
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