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
using Ninja.Domain.Entities.UserModel;

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
            var userToUpdate = new User() {Email = request.User.Email, Name = request.User.Name, Id = request.Id};

            var user = await _unitOfWork.Users.Update(x => x.Id == request.Id, userToUpdate);

            if (user == null)
            {
                return Response.Fail404NotFound<UserVm>("User Not Found");
            }

            user.Email = request.User.Email;
            user.Name = request.User.Name;

            return Response.Ok200(new UserVm() {Id = user.Id, Name = user.Name, Email = user.Email});
        }
    }
}