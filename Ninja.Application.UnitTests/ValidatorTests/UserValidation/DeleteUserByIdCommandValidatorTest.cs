using Moq;
using Ninja.Application.Users.Commands;
using Ninja.Application.Validations;
using Ninja.Domain.Entities.AddressModel;
using Ninja.Domain.Entities.UserModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ninja.Application.UnitTests.ValidatorTests.UserValidation
{
    [TestFixture]
    public class DeleteUserByIdCommandValidatorTest : BaseUnitOfWorkTests
    {
        private User User;

        [SetUp]
        public void Setup()
        {
            User = new User()
            {
                Id = new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"),
                Name = "Emmanuel",
                Lastname = "Espinoza",
                Age = 24,
                Email = "emman@globant.com",
                Address = new List<Address>() { new Address() { Description = "At my house" } },
                TelephoneNumber = "6622003300"
            };
        }

        [Test]
        public void DeleteUserByIdCommand_Successfully()
        {
            var command = new DeleteUserByIdCommand(new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"));
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(User);
            var validator = new DeleteUserByIdCommandValidator(UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void DeleteUserByIdCommand_FailEmtpyId()
        {
            var command = new DeleteUserByIdCommand(new Guid());
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(User);
            var validator = new DeleteUserByIdCommandValidator(UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void DeleteUserByIdCommand_FailUserNotExist()
        {
            var command = new DeleteUserByIdCommand(new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"));
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>()));
            var validator = new DeleteUserByIdCommandValidator(UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }
    }
}
