using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Commands
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, Response<bool>>
    {
        public readonly IUnitOfWork _unitOfWork;

        public DeleteUserByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.FindSingle(x => x.UserId == request.Id);

            if (user == null)
            {
                return Response.Fail404NotFound<bool>("User Not Found");
            }

            await _unitOfWork.Users.Remove(us=> us.UserId == request.Id);

            return Response.Ok200<bool>(true);
        }
    }
}
