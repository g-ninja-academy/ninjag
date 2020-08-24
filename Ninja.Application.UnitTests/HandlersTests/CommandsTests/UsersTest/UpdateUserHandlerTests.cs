using System;
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
        [TestCase(1, "Name", "Email")]
        public void UpdateUser_Successfully(int id, string name, string email)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Predicate<User>>())).Returns(new User
            {
                UserId = 1,
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
        [TestCase(1, "Name", "Email")]
        public void UpdateUser_NotFound(int id, string name, string email)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Predicate<User>>())).Returns(value: default);

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