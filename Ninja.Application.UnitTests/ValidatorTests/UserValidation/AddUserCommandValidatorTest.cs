using Ninja.Application.Common.Models;
using Ninja.Application.Users.Commands;
using Ninja.Application.Validations;
using Ninja.Domain.Entities.AddressModel;
using Ninja.Domain.Entities.UserModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.UnitTests.ValidatorTests.UserValidation
{
    [TestFixture]
    public class AddUserCommandValidatorTest
    {
        public BasicUserVm GetUser()
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

        [Test]
        public void AddUserCommand_Successfully()
        {
            var command = new AddUserCommand() { User = GetUser() };
            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailNameLength()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Name = "a";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailEmptyName()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Name = "";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailEmailFormat()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Email = "emmanglobant.com";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailEmptyEmail()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Email = "";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailEmptyLastname()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Lastname = "";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailLastnameLength()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Lastname = "a";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailEmptyTelephoneNumber()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.TelephoneNumber = "";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailTelephoneNumberFormat()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.TelephoneNumber = "123456d@#2";

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailEmptyAge()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Age = 0;

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailAgeLessThanEighteen()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Age = 16;

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailEmptyAddress()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Address = new List<AddressVm>();

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailZeroAddress()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Address = new List<AddressVm>();
            
            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailAddressEmptyDescription()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Address = new List<AddressVm>() { new AddressVm() { Description = "" } };

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void AddUserCommand_FailAddressDescriptionLength()
        {
            var command = new AddUserCommand() { User = GetUser() };
            command.User.Address = new List<AddressVm>() { new AddressVm() { Description = "h" } };

            var validator = new AddUserCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
        }
    }
}
