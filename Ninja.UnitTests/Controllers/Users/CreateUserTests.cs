using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Users.Commands;
using NUnit.Framework;
using System;

namespace Ninja.Api.UnitTests.Controllers.Users
{
    [TestFixture]
    public class CreateUserTests
    {
        private Mock<IMediator> _mediator;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new UserController(_mediator.Object);
        }

        public UserVm SetUserResponse()
        {
            return new UserVm()
            {
                Email = "max@globant.com",
                Id = new Guid("80517e54-7c6f-4167-bcbe-1d4f804cd8d0"),
                Name = "Max"
            };
        }

        [Test]
        [TestCase("80517e54-7c6f-4167-bcbe-1d4f804cd8d0", "max@mail.com", "Max")]
        public void CreateUser_Successfully(Guid id, string email, string name)
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddUserCommand>(), default))
                .ReturnsAsync(Response.Ok200<UserVm>(SetUserResponse()));

            var result = _controller.Post(SetUserResponse());
            var objectResult = (ObjectResult)result.Result.Result;
            var user = (Response<UserVm>)objectResult.Value;

            Assert.IsInstanceOf<Response<UserVm>>(user);
            Assert.AreEqual(new Guid("80517e54-7c6f-4167-bcbe-1d4f804cd8d0"), user.Data.Id);
        }

        [Test]
        [TestCase("80517e54-7c6f-4167-bcbe-1d4f804cd8d0", "max@mail.com", "Max")]
        public void CreateUser_ThowsException(Guid id, string email, string name)
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddUserCommand>(), default))
                .Throws(new Exception());

            Assert.That(() => _controller.Post(SetUserResponse()), Throws.TypeOf<Exception>());
        }
    }
}