﻿using MediatR;
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
                Id = 1,
                Name = "Max"
            };
        }

        [Test]
        [TestCase(1,"max@mail.com","Max")]
        public void CreateUser_Successfully(int id, string email, string name)
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddUserCommand>(), default))
                .ReturnsAsync(Response.Ok200<UserVm>(SetUserResponse()));            

            var result = _controller.Post(SetUserResponse());
            var objectResult = (ObjectResult)result.Result.Result;
            var user = (Response<UserVm>)objectResult.Value;

            Assert.IsInstanceOf<Response<UserVm>>(user);
            Assert.AreEqual(1, user.Data.Id);
        }

        [Test]
        [TestCase(1, "max@mail.com", "Max")]
        public void CreateUser_ThowsException(int id, string email, string name)
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddUserCommand>(), default))
                .Throws(new Exception());

            Assert.That(() => _controller.Post(SetUserResponse()), Throws.TypeOf<Exception>());
        }

    }
}
