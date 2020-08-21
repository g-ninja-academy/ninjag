using Moq;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.UnitTests
{
    public class BaseUnitOfWorkTests
    {
        public Mock<IUnitOfWork> UnitOfWorkMock { get;  }
        public Mock<IRepository<User>> UsersRespositoryMock { get; }
        public BaseUnitOfWorkTests()
        {
            UsersRespositoryMock = new Mock<IRepository<User>>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkMock.Setup(x => x.Users).Returns(UsersRespositoryMock.Object);
        }
    }
}
