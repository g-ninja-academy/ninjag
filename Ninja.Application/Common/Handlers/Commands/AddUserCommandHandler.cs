using MediatR;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Commands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<UserVm>>
    {
        public readonly IUsersService _userServiceRespository;

        public AddUserCommandHandler(IUsersService userServiceRespository)
        {
            _userServiceRespository = userServiceRespository;
        }

        public async Task<Response<UserVm>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return Response.Ok200(_userServiceRespository.CreateUser(request.UserViewModel));
        }
    }
}
