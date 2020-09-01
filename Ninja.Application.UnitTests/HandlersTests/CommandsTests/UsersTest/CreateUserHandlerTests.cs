using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Commands;
using Ninja.Application.Common.Models;
using Ninja.Application.Users.Commands;
using NUnit.Framework;

namespace Ninja.Application.UnitTests.HandlersTests.CommandsTests.UsersTest
{
    [TestFixture]
    public class CreateUserHandlerTests : BaseUnitOfWorkTests
    {
        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6", "Name", "Email")]
        public void CreateUser_Successfully(Guid id, string name, string email)
        {
            var handler = new AddUserCommandHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new AddUserCommand()
            {
                UserViewModel = new UserVm()
                {
                    Id = id,
                    Name = name,
                    Email = email
                }
            }, default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<UserVm>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }
    }
}