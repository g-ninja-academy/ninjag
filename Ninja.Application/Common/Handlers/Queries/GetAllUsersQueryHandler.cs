using MediatR;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserVm>>>
    {
        private readonly IUsersService _userServiceRespository;

        public GetAllUsersQueryHandler(IUsersService userServiceRespository)
        {
            _userServiceRespository = userServiceRespository;
        }

        public async Task<Response<IEnumerable<UserVm>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return Response.Ok200(_userServiceRespository.GetUsers());
        }
    }
}
