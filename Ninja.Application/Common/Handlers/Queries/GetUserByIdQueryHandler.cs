using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;

namespace Ninja.Application.Common.Handlers.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserVm>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserVm>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.FindSingle(x => x.UserId == request.UserId);
            if (user == null)
            {
                return Response.Fail404NotFound<UserVm>("User Not Found");
            }

            return Response.Ok200(new UserVm
            {
                Id = user.UserId,
                Name = user.Name,
                Email = user.Email
            });
        }
    }
}