using Moq;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.OrderModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Infrastructure.UnitTests.ServicesTests.OrderServiceTests
{
    [TestFixture]
    public class GetAllOrdersTests
    {
        private Mock<IOrderService> _serviceMock;
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IOrderService>();
        }
        [Test]
        public async Task GetAllOrders_Successfully()
        {
            _serviceMock.Setup(s => s.GetOrders()).ReturnsAsync(new List<Order>().AsEnumerable());
            var result = await _serviceMock.Object.GetOrders();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Order>>(result);
        }
    }
}
