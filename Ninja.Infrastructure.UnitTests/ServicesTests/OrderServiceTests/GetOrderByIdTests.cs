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
    public class GetOrderByIdTests
    {
        private Mock<IOrderService> _serviceMock;
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IOrderService>();
        }
        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6")]
        public async Task GetOrderById_Successfully(string orderId)
        {
            _serviceMock.Setup(s => s.GetOrderById(It.IsAny<Guid>())).ReturnsAsync(new Order());
            var result = await _serviceMock.Object.GetOrderById(new Guid(orderId));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Order>(result);
        }
        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6")]
        public async Task GetOrderById_NoFound(string orderId)
        {
            _serviceMock.Setup(s => s.GetOrderById(It.IsAny<Guid>()));
            var result = await _serviceMock.Object.GetOrderById(new Guid(orderId));

            Assert.IsNull(result);
        }
    }
}
