using Moq;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.OrderModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Infrastructure.UnitTests.ServicesTests.OrderServiceTests
{
    [TestFixture]
    public class CreateOrderTests
    {
        private Mock<IOrderService> _serviceMock;
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IOrderService>();
        }
        [Test]
        public async Task CreateOrder_Successfully()
        {
            _serviceMock.Setup(s => s.CreateOrder(It.IsAny<CreateOrder>())).ReturnsAsync(Guid.NewGuid());
            var result = await _serviceMock.Object.CreateOrder(new CreateOrder());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Guid>(result);
        }
    }
}
