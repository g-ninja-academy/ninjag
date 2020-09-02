using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.UserModel;
using Ninja.Infrastructure.Persistence.Repositories;
using NUnit.Framework;

namespace Ninja.Infrastructure.UnitTests.Persistence.Repository
{
    [TestFixture]
    class RepositoryTest
    {
        private Mock<IRepository<User>> repository;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository<User>>();
        }

        [Test]
        public void GetAll_WhenCalled_ReturnIEnumerable()
        {
            var result = repository.Object.GetAll();

            Assert.IsInstanceOf<IEnumerable<User>>(result);
        }

        [Test]
        public void FindSingle_WhenCalled_ReturnObject()
        {
            repository.Object.Add(new User() { Email = "emmanuel@globant.com", Name = "emmanuel" });

            var result = repository.Object.FindSingle(user => user.Email == "emmanuel@globant.com");

            Assert.IsInstanceOf<Task<User>>(result);
        }

        [Test]
        public void SearchBy_WhenCalled_ReturnIEnumerable()
        {
            repository.Object.Add(new User() { Email = "emmanuel@globant.com", Name = "emmanuel" });
            var restult = repository.Object.SearchBy(user => user.Name == "emmanuel");

            Assert.IsInstanceOf<IEnumerable<User>>(restult);
        }
    }
}