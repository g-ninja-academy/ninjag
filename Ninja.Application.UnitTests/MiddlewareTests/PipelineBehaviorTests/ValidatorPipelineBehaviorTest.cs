using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Commands;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Middlewares;
using Ninja.Application.Users.Commands;
using Ninja.Application.Validations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.UnitTests.MiddlewareTests.PipelineBehaviorTests
{
    class ValidatorPipelineBehaviorTest
    {
        Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Setup() 
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        [TestCase("")]
        public void ShouldReturnErrorWithEmptyName(string name) 
        {
            var command = new AddUserCommand()
            {
                User = new BasicUserVm()
                {
                    Name = "Roberto",
                    Lastname = "Hazael",
                    Email = "roberto.lopez@globant.com",
                    TelephoneNumber = "1245784512",
                    Age = 31,
                    Address = new List<AddressVm>() { new AddressVm() { Description = "Some description" } }
                }
            };
            
            //AddUserCommandHandler commandHandler = new AddUserCommandHandler(_unitOfWork.Object);
            //var validation = new ValidatorPipelineBehavior<AddUserCommand, Response<UserVm>>(new List<AddUserCommandValidator>()) { _validators = new AddUserCommandValidator() };
            AddUserCommandValidator validator = new AddUserCommandValidator();
            var result =  validator.Validate(command);
            //ValidatorPipelineBehavior<AddUserCommand,Response<UserVm>> validatorPipeline = new ValidatorPipelineBehavior();

            Assert.Equals(0,result.Errors.Count);

        }

    }
}
