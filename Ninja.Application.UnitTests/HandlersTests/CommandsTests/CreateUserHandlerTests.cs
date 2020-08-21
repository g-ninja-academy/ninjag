using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Commands;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Users.Commands;
using Ninja.Domain.Entities.UserModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.HandlersTests.CommandsTests
{
    [TestFixture]
    public class CreateUserHandlerTests : BaseUnitOfWorkTests
    {
        [Test]
        [TestCase(1,"Name","Email")]
        public void CreateUser_Successfully(int id, string name, string email)
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
