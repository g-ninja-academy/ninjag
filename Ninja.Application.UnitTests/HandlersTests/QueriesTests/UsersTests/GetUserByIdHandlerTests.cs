using System;
using System.Linq.Expressions;
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
        [TestCase("77c92558-832b-4cbe-99d2-f77813ced2e4")]
        public void GetUserById_Successfully(Guid id)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User
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
        [TestCase("77c92558-832b-4cbe-99d2-f77813ced2e4")]
        public void GetUserById_NotFound(Guid id)
        {
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(value: default);

            var handler = new GetUserByIdQueryHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new GetUserByIdQuery(id), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<UserVm>>>(result);
            Assert.AreEqual(StatusCodes.Status404NotFound, result.Result.StatusCode);
        }
    }
}