using MediatR;
using Ninja.Application.Common.Interfaces;
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
        public readonly IUnitOfWork _unitOfWork;

        public UpdateUserByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<UserVm>> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.FindSingle(x => x.UserId == request.Id);

            if (user == null)
            {
                return Response.Fail404NotFound<UserVm>("User Not Found");
            }

            user.Email = request.User.Email;
            user.Name = request.User.Name;

            return Response.Ok200(new UserVm() { Id = user.UserId, Name = user.Name, Email = user.Email });
        }
    }
}
