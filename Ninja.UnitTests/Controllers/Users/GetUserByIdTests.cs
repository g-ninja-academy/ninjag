using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
               .ReturnsAsync(Response.Ok200(GetUserResponse()));

            var result = _controller.Get(id);
            var objectResult = (ObjectResult)result.Result.Result;
            var data = (Response<UserVm>)objectResult.Value;
            var user = data.Data;

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.IsInstanceOf<UserVm>(user);
            Assert.AreEqual(1, user.Id);

        }

        [Test]
        [TestCase(1)]
        public void GetUserById_NotFound(int id)
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default))
               .ReturnsAsync(Response.Fail404NotFound<UserVm>(""));

            var result = _controller.Get(id);
            var objectResult = (ObjectResult)result.Result.Result;

            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

    }
}
