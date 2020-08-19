using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Application.Common.Models;
using Ninja.Api.Controllers;
using Ninja.Application.Services;
using NUnit.Framework;
using System;

namespace Ninja.Api.UnitTests.Controllers.Users
{
    [TestFixture]
    public class UpdateUserTests
    {
        private Mock<IUserServiceRepository> _service;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUserServiceRepository>();
            _controller = new UserController(_service.Object);
        }

        public User SetUserResponse()
        {
            return new User()
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
            _service.Setup(m => m.UpdateUser(It.IsAny<int>(),It.IsAny<User>())).Returns(SetUserResponse());

            var result = _controller.Put(id, SetUserResponse());
            var objectResult = (ObjectResult)result.Result;
            var user = (User)objectResult.Value;

            Assert.IsInstanceOf<User>(user);
            Assert.AreEqual(1, user.Id);
        }
        [Test]
        [TestCase(1)]
        public void UpdateUserById_ThrowsException(int id)
        {
            _service.Setup(m => m.UpdateUser(It.IsAny<int>(), It.IsAny<User>())).Throws(new Exception());

            Assert.That(() => _controller.Put(id,SetUserResponse()), Throws.TypeOf<Exception>());
        }
    }
}
