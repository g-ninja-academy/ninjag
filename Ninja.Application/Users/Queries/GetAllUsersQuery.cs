using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Response<IEnumerable<UserVm>>>
    {

    }
}
