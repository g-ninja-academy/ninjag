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
        public readonly int _userId;

        public GetUserByIdQuery(int userId)
        {
            _userId = userId;
        }
    }
}
