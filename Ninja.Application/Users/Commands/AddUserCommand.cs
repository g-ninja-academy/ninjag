using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;

namespace Ninja.Application.Users.Commands
{
    public class AddUserCommand : IRequest<Response<UserVm>>
    {
        public BasicUserVm UserViewModel { get; set; }
    }
}