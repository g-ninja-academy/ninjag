using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Users.Queries;

namespace Ninja.Api.UnitTests.Controllers.Users
{
    [TestFixture]
    public class GetUserByIdTests
    {
        private Mock<IMediator> _mediator;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new UserController(_mediator.Object);
        }

        public UserVm GetUserResponse()
        {
            return new UserVm()
            {
                Email = "max@globant.com",
                Id = 1,
                Name = "Max"
            };
        }

        [Test]
        [TestCase(1)]
        public void GetUserById_Successfully(int id)
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default))
                .ReturnsAsync(Response.Ok200<UserVm>(GetUserResponse()));

            var result = _controller.Get(id);
            var objectResult = (ObjectResult) result.Result.Result;
            var user = (Response<UserVm>) objectResult.Value;

            Assert.IsInstanceOf<Response<UserVm>>(user);
            Assert.AreEqual(1, user.Data.Id);
        }

        [Test]
        [TestCase(1)]
        public void GetUserById_ThrowsException(int id)
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default)).Throws(new Exception());

            Assert.That(() => _controller.Get(id), Throws.TypeOf<Exception>());
        }
    }
}