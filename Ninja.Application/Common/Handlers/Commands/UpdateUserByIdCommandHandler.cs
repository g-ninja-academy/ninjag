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
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, Response<UserVm>>
    {
        public readonly IUserServiceRepository _userServiceRespository;

        public UpdateUserByIdCommandHandler(IUserServiceRepository userServiceRespository)
        {
            _userServiceRespository = userServiceRespository;
        }
        public async Task<Response<UserVm>> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            return Response.Ok200(_userServiceRespository.UpdateUser(request.Id, request.User));
        }
    }
}
