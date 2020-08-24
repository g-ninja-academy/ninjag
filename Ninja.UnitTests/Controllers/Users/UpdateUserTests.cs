using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Application.Common.Models;
using Ninja.Api.Controllers;
using Ninja.Application.Users.Commands;
using NUnit.Framework;
using System;
using MediatR;
using Ninja.Application.Common;
using Microsoft.AspNetCore.Http;

namespace Ninja.Api.UnitTests.Controllers.Users
{
    [TestFixture]
    public class UpdateUserTests
    {
        private Mock<IMediator> _mediator;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new UserController(_mediator.Object);
        }

        public BasicUserVm SetBasicUserResponse()
        {
            return new BasicUserVm()
            {
                Email = "max@globant.com",
                Name = "Max"
            };
        }

        public UserVm SetUser()
        {
            return new UserVm()
            {
                Id = 1,
                Email = "somename@globant.com",
                Name = "somename"
            };
        }

        [Test]
        [TestCase(1)]
        public void UpdateUserById_Successfully(int id)
        {
            _mediator.Setup(m => m.Send(It.IsAny<UpdateUserByIdCommand>(), default))
                .ReturnsAsync(Response.Ok200<UserVm>(SetUser()));

            var result = _controller.Put(id, SetBasicUserResponse());
            var objectResult = (ObjectResult) result.Result.Result;
            var user = (Response<UserVm>) objectResult.Value;

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Response<UserVm>>(user);
            Assert.AreEqual(1, user.Data.Id);
        }

        [Test]
        [TestCase(1)]
        public void UpdateUserById_NotFound(int id)
        {
            _mediator.Setup(m => m.Send(It.IsAny<UpdateUserByIdCommand>(), default))
                .ReturnsAsync(Response.Fail404NotFound<UserVm>(""));

            var result = _controller.Put(id, SetBasicUserResponse());
            var objectResult = (ObjectResult)result.Result.Result;

            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
        [Test]
        [TestCase(1)]
        public void UpdateUserById_ThrowsException(int id)
        {
            _mediator.Setup(m => m.Send(It.IsAny<UpdateUserByIdCommand>(), default)).Throws(new Exception());

            Assert.That(() => _controller.Put(id, SetBasicUserResponse()), Throws.TypeOf<Exception>());
        }
    }
}