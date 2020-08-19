using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;

namespace Ninja.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public readonly IUserServiceRepository _userServiceRespository;

        public UserController(IUserServiceRepository userServiceRespository)
        {
            _userServiceRespository = userServiceRespository;
        }
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<UserVm>>>> Get()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return StatusCode(result.StatusCode, result);
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