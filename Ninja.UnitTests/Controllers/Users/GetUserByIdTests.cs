﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Api.Common.Models;
using Ninja.Api.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.UnitTests.Controllers.Users
{

    [TestFixture]
    public class GetUserByIdTests
    {
        private Mock<IUserServiceRepository> _service;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUserServiceRepository>();            
            _controller = new UserController(_service.Object);
        }

        public User GetUserResponse()
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
        public void GetUserById_Successfully(int id)
        {
            _service.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(GetUserResponse());

            var result = _controller.Get(id);
            var objectResult = (ObjectResult)result.Result;
            var user = (User)objectResult.Value;

            Assert.IsInstanceOf<User>(user);
            Assert.AreEqual(1, user.Id);
        }

        [Test]
        [TestCase(1)]
        public void GetUserById_ThrowsException(int id)
        {
            _service.Setup(m => m.GetUserById(It.IsAny<int>())).Throws(new Exception());

            Assert.That(() => _controller.Get(id), Throws.TypeOf<Exception>());
        }
    }
}