using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Commands;
using Ninja.Application.Users.Commands;
using Ninja.Domain.Entities.UserModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.HandlersTests.CommandsTests.UsersTest
{
    [TestFixture]
    public class DeleteUserHandlerTests: BaseUnitOfWorkTests
    {
        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6", "Name", "Email")]
        public void DeleteUser_Successfully(Guid id, string name, string email)
        {

            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User
            {
                UserId = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Name = "",
                Email = ""
            });

            base.UsersRespositoryMock.Setup(x => x.Remove(It.IsAny<Expression<Func<User, bool>>>()));

            var handler = new DeleteUserByIdCommandHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new DeleteUserByIdCommand(id), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<bool>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }

        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6", "Name", "Email")]
        public void DeleteUser_NotFound(Guid id, string name, string email)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(value: default);

            var handler = new DeleteUserByIdCommandHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new DeleteUserByIdCommand(id), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<bool>>>(result);
            Assert.AreEqual(StatusCodes.Status404NotFound, result.Result.StatusCode);
        }
    }
}
