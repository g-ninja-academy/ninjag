using Microsoft.AspNetCore.Http;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Queries;
using Ninja.Application.Common.Models;
using Ninja.Application.Users.Queries;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.QueriesTests
{
    [TestFixture]
    public class GetAllUsersHandlerTests : BaseUnitOfWorkTests
    {
        [Test]
        public void GetAllUsers_Successfully() 
        {
            var handler = new GetAllUsersQueryHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new GetAllUsersQuery(), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<IEnumerable<UserVm>>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }
    }
}
