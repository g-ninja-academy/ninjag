using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Commands;
using Ninja.Domain.Entities.AddressModel;
using Ninja.Domain.Entities.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
            User user = new User
            {               
                Name = request.User.Name,
                Lastname = request.User.Lastname,
                Email = request.User.Email,
                TelephoneNumber = request.User.TelephoneNumber,
                Address = request.User.Address.Select(adress => new Address() { Description = adress.Description }).ToList(),
                Age = request.User.Age
            };

            await _unitOfWork.Users.Add(user);

            UserVm userResult = new UserVm()
            {
                Id = user.Id,
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                TelephoneNumber = user.TelephoneNumber,
                Address = user.Address.Select(adress => new AddressVm() { Description = adress.Description }).ToList(),
                Age = request.User.Age
            };

            return Response.Ok200(userResult);
        }
    }
}