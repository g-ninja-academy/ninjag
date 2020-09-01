using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Commands;
using Ninja.Application.Users.Queries;

namespace Ninja.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method that retrieves the entire list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<List<UserVm>>>> Get()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Method that retrieves only 1 user by Id
        /// </summary>
        /// <param name="id">User Identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<UserVm>>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserVm>> Post([FromBody] BasicUserVm user)
        {
            var result = await _mediator.Send(new AddUserCommand() {UserViewModel = user});
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Method for update an user record 
        /// </summary>
        /// <param name="id">User Identifier</param>
        /// <param name="user">New user info like Name or Email</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserVm>> Put(Guid id, [FromBody] BasicUserVm user)
        {
            var result = await _mediator.Send(new UpdateUserByIdCommand(id, user));
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Method that delete only 1 user by Id
        /// </summary>
        /// <param name="id">User Identifier</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<UserVm>>> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserByIdCommand(id)); ;
            return StatusCode(result.StatusCode, result);
        }
    }
}