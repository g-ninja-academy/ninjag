using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using MediatR;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Commands;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Middlewares;
using Ninja.Application.Users.Commands;
using Ninja.Application.Validations;
using NUnit.Framework;

namespace Ninja.Application.UnitTests.MiddlewareTest
{
    [TestFixture]
    class ValidatorPipelineBehaviorTest
    {
        private Mock<IValidator<AddUserCommand>> _validator;
        private Mock<RequestHandlerDelegate<Response<UserVm>>> _delegate;
        private List<IValidator<AddUserCommand>> _listValidators;
        private List<IValidator<AddUserCommand>> _listValidatorsEmpty;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<IValidator<AddUserCommand>>();
            _listValidators = new List<IValidator<AddUserCommand>>() {_validator.Object};
            _listValidatorsEmpty = new List<IValidator<AddUserCommand>>() { };
            _delegate = new Mock<RequestHandlerDelegate<Response<UserVm>>>();
        }

        [Test]
        public async Task ValidatorPipelineBehavior_Success_ReturnNoErrors()
        {
            var validatorPipeline =
                new ValidatorPipelineBehavior<AddUserCommand, Response<UserVm>>(_listValidatorsEmpty.AsEnumerable());

            _delegate.Setup(m => m());

            var result = await validatorPipeline.Handle(new AddUserCommand(), default, _delegate.Object);

            _delegate.Verify(m => m(), Times.AtLeastOnce);
        }

        [Test]
        public async Task ValidatorPipelineBehavior_ItHasValidators_ReturnNoErrors()
        {
            _validator.Setup(m => m.Validate(It.IsAny<ValidationContext<AddUserCommand>>()))
                .Returns(new ValidationResult());

            var validatorPipeline =
                new ValidatorPipelineBehavior<AddUserCommand, Response<UserVm>>(_listValidators.AsEnumerable());

            _delegate.Setup(m => m());


            var result = await validatorPipeline.Handle(new AddUserCommand(), default, _delegate.Object);

            _delegate.Verify(m => m(), Times.AtLeastOnce);
        }

        [Test]
        public async Task ValidatorPipelineBehavior_ItHasValidators_ReturnErrors()
        {
            _validator.Setup(m => m.Validate(It.IsAny<ValidationContext<AddUserCommand>>()))
                .Returns(
                    new ValidationResult(
                        new List<ValidationFailure>()
                        {
                            new ValidationFailure("Name", "The name can not be empty")
                        }));

            var validatorPipeline =
                new ValidatorPipelineBehavior<AddUserCommand, Response<UserVm>>(_listValidators.AsEnumerable());

            _delegate.Setup(m => m());

            Assert.That(() => validatorPipeline.Handle(new AddUserCommand(), default, _delegate.Object),
                Throws.TypeOf<ValidationException>());
        }
    }
}