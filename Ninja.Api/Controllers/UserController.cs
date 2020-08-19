using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ninja.Api.Common.Models;
using Ninja.Api.Services;

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
        public ActionResult<User> Get(int id)
        {
            return Ok(_userServiceRespository.GetUserById(id));
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            return Ok(_userServiceRespository.CreateUser(user));
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            return Ok(_userServiceRespository.UpdateUser(id, user));
        }
    }
}