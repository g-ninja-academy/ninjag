using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Queries;
using Ninja.Application.Common.Models;
using Ninja.Application.Users.Queries;
using Ninja.Domain.Entities.UserModel;
using NUnit.Framework;

namespace Ninja.Application.UnitTests.HandlersTests.QueriesTests.UsersTests
{
    [TestFixture]
    public class GetUserByIdHandlerTests : BaseUnitOfWorkTests
    {
        [Test]
        [TestCase(1)]
        public void GetUserById_Successfully(int id)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Predicate<User>>())).Returns(new User
            {
                UserId = id,
                Name = "test",
                Email = "test"
            });

            var handler = new GetUserByIdQueryHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new GetUserByIdQuery(id), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<UserVm>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }

        [Test]
        [TestCase(1)]
        public void GetUserById_NotFound(int id)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Predicate<User>>())).Returns(value: default);

            var handler = new GetUserByIdQueryHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new GetUserByIdQuery(id), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<UserVm>>>(result);
            Assert.AreEqual(StatusCodes.Status404NotFound, result.Result.StatusCode);
        }
    }
}