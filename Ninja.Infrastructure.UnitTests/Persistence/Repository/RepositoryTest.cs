using System;
using System.Collections.Generic;
using System.Text;
using Ninja.Domain.Entities.UserModel;
using Ninja.Infrastructure.Persistence.Repositories;
using NUnit.Framework;

namespace Ninja.Infrastructure.UnitTests.Persistence.Repository
{
    [TestFixture]
    class RepositoryTest
    {
        private List<User> users = new List<User>();
        private Repository<User> repository;

        [SetUp]
        public void SetUp()
        {
            users.Add(new User() {UserId = 1, Email = "roberto@globant.com", Name = "Roberto"});
            repository = new Repository<User>(users);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnIEnumerable()
        {
            var result = repository.GetAll();

            Assert.IsInstanceOf<IEnumerable<User>>(result);
        }

        [Test]
        public void FindSingle_WhenCalled_ReturnObject()
        {
            var result = repository.FindSingle(user => user.UserId == 1);

            Assert.IsInstanceOf<User>(result);
        }

        [Test]
        public void SearchBy_WhenCalled_ReturnIEnumerable()
        {
            var restult = repository.SearchBy(user => user.Name == "Roberto");

            Assert.IsInstanceOf<IEnumerable<User>>(restult);
        }
    }
}