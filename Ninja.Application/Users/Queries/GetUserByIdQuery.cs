using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;

namespace Ninja.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Response<UserVm>>
    {
        public Guid UserId { get; }

        public GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
