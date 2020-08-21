using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Commands;
using Ninja.Domain.Entities.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Commands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<UserVm>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserVm>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User { UserId = request.UserViewModel.Id, Name = request.UserViewModel.Name, Email = request.UserViewModel.Email }
            _unitOfWork.Users.Add(user);
            
            return Response.Ok200(request.UserViewModel);
        }
    }
}
