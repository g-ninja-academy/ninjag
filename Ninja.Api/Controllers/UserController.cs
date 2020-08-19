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
        public ActionResult<List<UserVm>> Get()
        {
            return Ok(_userServiceRespository.GetUsers());
        }
        
        [HttpGet("{id}")]
        public ActionResult<UserVm> Get(int id)
        {
            return Ok(_userServiceRespository.GetUserById(id));
        }

        [HttpPost]
        public ActionResult<UserVm> Post([FromBody] UserVm user)
        {
            return Ok(_userServiceRespository.CreateUser(user));
        }

        [HttpPut("{id}")]
        public ActionResult<UserVm> Put(int id, [FromBody] UserVm user)
        {
            return Ok(_userServiceRespository.UpdateUser(id, user));
        }
    }
}