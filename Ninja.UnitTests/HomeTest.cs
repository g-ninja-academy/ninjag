using Ninja.Api.Controllers;
using Ninja.Application.Common.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;
using Ninja.Application.Services;

namespace Ninja.UnitTests
{
    public class HomeTest
    {
        private Mock<IUserServiceRepository> _service;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUserServiceRepository>();
            _controller = new UserController(_service.Object);
        }

        [Test]
        public void GetUsers_ReturnAllUsers_ListOfUsers()
        {
            _service.Setup(m => m.GetUsers()).Returns(new List<User>());

            var result = _controller.Get();

            Assert.That(result, Is.TypeOf<List<User>>());
        }

        [Test]
        public void GetUserById_ReturnUser_SingleUserObject()
        {
        
            var result = _controller.Get(1);

            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public void PostUser_SaveNewUser_ReurnUserObject()
        {
            var user = new User(3, "Max", "max@globant.com");

            var lastUser = _controller.Post(user);

            Assert.AreEqual(lastUser.Id, user.Id);
        }

        [Test]
        public void UpdateUser_UpdateUserName_ReturnUserNameUpdated()
        {
            var newName = "Hazael";
            var userId = 1;
            var userInfo = new User(1, newName, "roberto@globant.com");

            var userUpdated = _controller.Put(userId, userInfo);

            Assert.AreEqual(userUpdated.Name, newName);
        }
    }
}