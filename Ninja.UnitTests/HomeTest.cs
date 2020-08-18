using Ninja.Api.Controllers;
using Ninja.Application.Common.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Ninja.UnitTests
{
    public class HomeTest
    {
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new HomeController();
        }

        [Test]
        public void GetUsers_ReturnAllUsers_ListOfUsers()
        {
            // Act
            var result = _controller.Get();
            // Assert
            Assert.That(result, Is.TypeOf<List<User>>());
        }

        [Test]
        public void GetUserById_ReturnUser_SingleUserObject()
        {
            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public void PostUser_SaveNewUser_ReurnUserObject()
        {
            // Arrenge
            var user = new User(3, "Max", "max@globant.com");

            // Act
            var lastUser = _controller.Post(user);

            // Assert
            Assert.AreEqual(lastUser.Id, user.Id);
        }

        [Test]
        public void UpdateUser_UpdateUserName_ReturnUserNameUpdated()
        {
            // Arrenge
            var newName = "Hazael";
            var userId = 1;
            var userInfo = new User(1, newName, "roberto@globant.com");

            // Act
            var userUpdated = _controller.Put(userId, userInfo);

            // Assert
            Assert.AreEqual(userUpdated.Name, newName);
        }
    }
}