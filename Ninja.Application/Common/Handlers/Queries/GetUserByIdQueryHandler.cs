using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;

namespace Ninja.Application.Common.Handlers.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserVm>>
    {
        private readonly IUsersService _userServiceRepository;

        public GetUserByIdQueryHandler(IUsersService userServiceRepository)
        {
            _userServiceRepository = userServiceRepository;
        }

        public async Task<Response<UserVm>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return Response.Ok200(_userServiceRepository.GetUserById(request._userId));
        }
    }
}