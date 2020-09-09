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
using System.Linq;
using Ninja.Domain.Entities.AddressModel;

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
            var user = await _unitOfWork.Users.FindSingle(x => x.Id == request.Id);

            if (user == null)
            {
                return Response.Fail404NotFound<UserVm>("User Not Found");
            }

            var userToUpdate = new User()
            {
                Name = request.User.Name, 
                Lastname = request.User.Lastname,
                Email = request.User.Email, 
                Age = request.User.Age,
                TelephoneNumber = request.User.TelephoneNumber,
                Address = request.User.Address.Select(adress => new Address() { Description = adress.Description }).ToList(),
            };

            var userResult = await _unitOfWork.Users.Update(x => x.Id == request.Id, userToUpdate);

            UserVm userResponse = new UserVm()
            {
                Id = userResult.Id,
                Name = userResult.Name,
                Lastname = userResult.Lastname,
                Age = userResult.Age,
                Email = userResult.Email,
                TelephoneNumber = userResult.TelephoneNumber,
                Address = userResult.Address.Select(adress => new AddressVm() { Description = adress.Description }).ToList(),
            };

            return Response.Ok200(userResponse);
        }
    }
}