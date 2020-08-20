using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common.Models;
using Ninja.Application.Services;
using NUnit.Framework;

namespace Ninja.Api.UnitTests.Controllers.Users
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

        public List<UserVm> GetUsersResponse()
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
            _service.Setup(m => m.GetUsers()).Returns(GetUsersResponse());

            var result = _controller.Get();
            var objectResult = (ObjectResult)result.Result;
            var listUsers = (List<UserVm>)objectResult.Value;

            Assert.IsInstanceOf<List<UserVm>>(listUsers);
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