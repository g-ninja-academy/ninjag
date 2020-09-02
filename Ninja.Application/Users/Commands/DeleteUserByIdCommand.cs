using MediatR;
using Ninja.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Users.Commands
{
    public class DeleteUserByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; }
        public DeleteUserByIdCommand(Guid id)
        {
            Id = id;
        }

    }
}
