using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Users.Commands
{
    public class UpdateUserByIdCommand : IRequest<Response<UserVm>>
    {
        public readonly int Id;
        public readonly BasicUserVm User;

        public UpdateUserByIdCommand(int id, BasicUserVm user)
        {
            Id = id;
            User = user;
        }
    }
}