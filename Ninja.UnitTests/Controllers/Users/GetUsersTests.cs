using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;
using NUnit.Framework;

namespace Ninja.Api.UnitTests.Controllers.Users
{
    [TestFixture]
    public class GetUsersTests
    {
        private Mock<IMediator> _mediator;

        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new UserController(_mediator.Object);
        }

        public IEnumerable<UserVm> GetUsersResponse()
        {
            return new List<UserVm>()
            {
                new UserVm()
                {
                    Email = "max@globant.com", Id = 1, Name = "Max"
                }
            };
        }

        [Test]
        public void GetUsers_ReturnAllUsers_Successfully()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default))
                .ReturnsAsync(Response.Ok200<IEnumerable<UserVm>>(GetUsersResponse()));

            var result = _controller.Get();
            var objectResult = (ObjectResult) result.Result.Result;
            var listUsers = (Response<IEnumerable<UserVm>>) objectResult.Value;

            Assert.IsInstanceOf<Response<IEnumerable<UserVm>>>(objectResult.Value);
            Assert.AreEqual(1, listUsers.Data.Count());
        }

        [Test]
        public void GetUsers_ThrowAnException()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default))
                .Throws(new Exception());

            Assert.That(() => _controller.Get(), Throws.TypeOf<Exception>());
        }
    }
}