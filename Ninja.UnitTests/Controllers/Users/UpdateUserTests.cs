using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Application.Common.Models;
using Ninja.Api.Controllers;
using Ninja.Application.Users.Commands;
using NUnit.Framework;
using System;
using MediatR;
using Ninja.Application.Common;

namespace Ninja.Api.UnitTests.Controllers.Users
{
    [TestFixture]
    public class UpdateUserTests
    {
        //private Mock<IUserServiceRepository> _service;
        private Mock<IMediator> _mediator;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            //_service = new Mock<IUserServiceRepository>();
            //_controller = new UserController(_service.Object);
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
        [TestCase(1)]
        public void UpdateUserById_Successfully(int id)
        {
            //_service.Setup(m => m.UpdateUser(It.IsAny<int>(),It.IsAny<UserVm>())).Returns(SetUserResponse());
            _mediator.Setup(m => m.Send(It.IsAny<UpdateUserByIdCommand>(), default)).ReturnsAsync(Response.Ok200<UserVm>(SetUserResponse()));

            var result = _controller.Put(id, SetUserResponse());
            //var objectResult = (ObjectResult)result.Result;
            //var user = (UserVm)objectResult.Value;
            var objectResult = (ObjectResult)result.Result.Result;
            var user = (Response<UserVm>)objectResult.Value;

            //Assert.IsInstanceOf<UserVm>(user);
            //Assert.AreEqual(1, user.Id);
            Assert.IsInstanceOf<Response<UserVm>>(user);
            Assert.AreEqual(1, user.Data.Id);
        }
        [Test]
        [TestCase(1)]
        public void UpdateUserById_ThrowsException(int id)
        {
            //_service.Setup(m => m.UpdateUser(It.IsAny<int>(), It.IsAny<UserVm>())).Throws(new Exception());
            _mediator.Setup(m => m.Send(It.IsAny<UpdateUserByIdCommand>(), default)).Throws(new Exception());

            Assert.That(() => _controller.Put(id,SetUserResponse()), Throws.TypeOf<Exception>());
        }
    }
}
