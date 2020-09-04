using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
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
    public class UpdateUserHandlerTests : BaseUnitOfWorkTests
    {
        private static List<User> GetUser()
        {
            var users = new List<User>();
            users.Add(new User()
            {
                Id = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Email = "SomeEmail",
                Name = "Name",
                Lastname = "Lastname",
                Age = 18,
                Address = new List<Address>() { new Address() { Description = "qwerty" } }
            });
            return users;
        }


        [Test,TestCaseSource("GetUser")]
        
        public void UpdateUser_Successfully(User user)
        {
            List<Address> address = new List<Address>();
            address.Add(new Address { Description = "" });
            base.UsersRespositoryMock.Setup(x => x.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User
            {
                Id = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Name = "",
                Email = "",
                Lastname = "",
                TelephoneNumber = "",
                Age = 0,
                Address = address 
            });

            base.UsersRespositoryMock.Setup(x => x.Update(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<User>())).ReturnsAsync(new User
            {
                Id = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Name = "",
                Email = "",
                Lastname = "",
                TelephoneNumber = "",
                Age = 0,
                Address = address
            });

            var handler = new UpdateUserByIdCommandHandler(base.UnitOfWorkMock.Object);

            var result = handler.Handle(new UpdateUserByIdCommand(user.Id,
                new BasicUserVm()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Lastname = user.Lastname,
                    TelephoneNumber = user.TelephoneNumber,
                    Age = user.Age,
                    Address = user.Address.Select(u => new AddressVm { Description = u.Description }).ToList()
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