﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Api.Common.Models;
using Ninja.Api.Services;
using NUnit.Framework;

namespace Ninja.UnitTests.Controllers.Users
{
    [TestFixture]
    public class GetUsersTests
    {
        private Mock<IUserServiceRepository> _service;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUserServiceRepository>();
            _controller = new UserController(_service.Object);
        }

        public List<User> GetUsersResponse()
        {
            return new List<User>()
            {
                new User() 
                {
                    Email = "max@globant.com", Id = 1, Name = "Max"
                }
            };
        }
        [Test]
        public void GetUsers_ReturnAllUsers_Successfully()
        {
            _service.Setup(m => m.GetUsers()).Returns(GetUsersResponse());

            var result = _controller.Get();
            var objectResult = (ObjectResult)result.Result;
            var listUsers = (List<User>)objectResult.Value;

            Assert.IsInstanceOf<List<User>>(listUsers);
            Assert.AreEqual(1, listUsers.Count);
        }

        [Test]
        public void GetUsers_ThrowAnException()
        {
            _service.Setup(m => m.GetUsers()).Throws(new Exception());

            Assert.That(() => _controller.Get(), Throws.TypeOf<Exception>());
        }
    }
}