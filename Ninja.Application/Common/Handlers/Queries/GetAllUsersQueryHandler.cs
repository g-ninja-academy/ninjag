using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserVm>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<UserVm>>> Handle(GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            List<UserVm> users = new List<UserVm>();
            _unitOfWork.Users.GetAll().ToList().ForEach(x =>
            {
                users.Add(new UserVm
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name
                });
            });

            return Response.Ok200<IEnumerable<UserVm>>(users);
        }
    }
}