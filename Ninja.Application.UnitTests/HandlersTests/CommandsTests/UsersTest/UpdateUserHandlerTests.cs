using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Commands;
using Ninja.Application.Common.Models;
using Ninja.Application.Users.Commands;
using Ninja.Domain.Entities.UserModel;
using NUnit.Framework;

namespace Ninja.Application.UnitTests.HandlersTests.CommandsTests.UsersTest
{
    [TestFixture]
    public class UpdateUserHandlerTests : BaseUnitOfWorkTests
    {
        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6", "Name", "Email")]
        public void UpdateUser_Successfully(Guid id, string name, string email)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User
            {
                Id = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Name = "",
                Email = ""
            });
            base.UsersRespositoryMock.Setup(x => x.Update(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<User>())).ReturnsAsync(new User
            {
                Id = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Name = "",
                Email = ""
            });

            var handler = new UpdateUserByIdCommandHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new UpdateUserByIdCommand(id,
                new BasicUserVm()
                {
                    Name = name,
                    Email = email
                }
            ), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<UserVm>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }

        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6", "Name", "Email")]
        public void UpdateUser_NotFound(Guid id, string name, string email)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(value: default);

            var handler = new UpdateUserByIdCommandHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new UpdateUserByIdCommand(id,
                new BasicUserVm()
                {
                    Name = name,
                    Email = email
                }
            ), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<UserVm>>>(result);
            Assert.AreEqual(StatusCodes.Status404NotFound, result.Result.StatusCode);
        }
    }
}