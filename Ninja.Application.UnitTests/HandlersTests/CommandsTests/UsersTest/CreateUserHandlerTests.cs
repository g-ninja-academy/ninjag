using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Commands;
using Ninja.Application.Common.Models;
using Ninja.Application.Users.Commands;
using Ninja.Domain.Entities.AddressModel;
using Ninja.Domain.Entities.UserModel;
using NUnit.Framework;

namespace Ninja.Application.UnitTests.HandlersTests.CommandsTests.UsersTest
{
    [TestFixture]
    public class CreateUserHandlerTests : BaseUnitOfWorkTests
    {
        private static List<User> GetUser()
        {
            var users = new List<User>();
            users.Add(new User()
            {
                Id = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Email = "SomeEmail",
                Lastname = "Lastname",
                Age = 18,
                Name = "Name",
                Address = new List<Address>() { new Address() { Description = "qwerty" } }
            });
            return users;
        }

        [Test, TestCaseSource("GetUser")]
        public void CreateUser_Successfully(User user)
        {
            var handler = new AddUserCommandHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new AddUserCommand()
            {
                User = new BasicUserVm()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Lastname = user.Lastname,
                    TelephoneNumber = user.TelephoneNumber,
                    Age = user.Age,
                    Address = user.Address.Select(u => new AddressVm { Description = u.Description }).ToList()
                }
            }, default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<UserVm>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }
    }
}