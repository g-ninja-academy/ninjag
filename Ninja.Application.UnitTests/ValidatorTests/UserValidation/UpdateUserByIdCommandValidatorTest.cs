using Moq;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
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
    public class UpdateUserByIdCommandValidatorTest : BaseUnitOfWorkTests
    {
        public BasicUserVm GetBasicUserVm()
        {
            return new BasicUserVm()
            {
                Name = "Emmanuel",
                Lastname = "Espinoza",
                Age = 24,
                Email = "emman@globant.com",
                Address = new List<AddressVm>() { new AddressVm() { Description = "At my house" } },
                TelephoneNumber = "6622003300"
            };
        }

        public User GetUser()
        {
            return new User()
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
        public void UpdateUserByIdCommand_Successfully()
        {
            var command = new UpdateUserByIdCommand(new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"), GetBasicUserVm());
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailUserNotExist()
        {
            var command = new UpdateUserByIdCommand(new Guid("f5d958ec-d760-4abe-bf3e-c8ba12c975e6"), GetBasicUserVm());
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>()));
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailIdNullOrEmpty()
        {
            var command = new UpdateUserByIdCommand(new Guid(), GetBasicUserVm());
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailUserNameNullOrEmpty()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Name = "";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailUserNameLength()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Name = "a";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailEmptyEmail()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Email = "";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailEmailFormat()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Email = "emman.com";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailLastNameEmpty()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Lastname = "";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailLastNameLength()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Lastname = "a";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailEmptyTelephoneNumber()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.TelephoneNumber = "";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailTelephoneNumberFormat()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.TelephoneNumber = "as124$%&";

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailEmptyAge()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Age = 0;

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailAgeLessThanEighteen()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Age = 16;

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailNullAddress()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Address = new List<AddressVm>();

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailZeroAddress()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Address = new List<AddressVm>();

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailEmptyAddressDescription()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Address = new List<AddressVm>() { new AddressVm() { Description = "" } };

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateUserByIdCommand_FailAddressDescriptionLength()
        {
            var userNameFail = GetBasicUserVm();
            userNameFail.Address = new List<AddressVm>() { new AddressVm() { Description = "a" } };

            var command = new UpdateUserByIdCommand(new Guid(), userNameFail);
            base.UsersRespositoryMock.Setup(user => user.FindSingle(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(GetUser);
            var validator = new UpdateUserByIdCommandValidator(base.UnitOfWorkMock.Object);

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

    }
}
