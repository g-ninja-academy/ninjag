﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.UnitTests.Controllers.Users
{
    [TestFixture]
    public class CreateUserTests
    {
        private Mock<IUserServiceRepository> _service;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUserServiceRepository>();
            _controller = new UserController(_service.Object);
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
            _service.Setup(m => m.CreateUser(It.IsAny<UserVm>())).Returns(SetUserResponse());

            var result = _controller.Post(SetUserResponse());
            var objectResult = (ObjectResult)result.Result;
            var user = (UserVm)objectResult.Value;

            Assert.IsInstanceOf<UserVm>(user);
            Assert.AreEqual(1, user.Id);
        }
        [Test]
        [TestCase(1, "max@mail.com", "Max")]
        public void CreateUser_ThowsException(int id, string email, string name)
        {
            _service.Setup(m => m.CreateUser(It.IsAny<UserVm>())).Throws(new Exception());

            Assert.That(() => _controller.Post(SetUserResponse()), Throws.TypeOf<Exception>());
        }

    }
}
