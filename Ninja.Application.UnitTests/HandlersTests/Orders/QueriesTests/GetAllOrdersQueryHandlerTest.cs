using Moq;
using Ninja.Application.Common.Handlers.Orders.Queries;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Orders.Commands;
using Ninja.Application.Orders.Queries;
using Ninja.Domain.Entities.OrderModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.HandlersTests.Orders.QueriesTests
{
    [TestFixture]
    public class GetAllOrdersQueryHandlerTest
    {
        public Mock<IOrderService> OrderServiceMock { get; set; }

        [SetUp]
        public void Setup()
        {
            OrderServiceMock = new Mock<IOrderService>();
        }

        private async Task<IEnumerable<Order>> GetOrders()
        {
            var products = new List<ProductOrder>() { new ProductOrder() { ProductId = Guid.NewGuid(), Price = 10, Quantity = 29 }};
            return new List<Order>() { new Order() { OrderId = Guid.NewGuid(), UserId = Guid.NewGuid(), Products = products } };
        }

        private async Task<IEnumerable<Order>> GetEmptyOrders()=> new List<Order>();

        [Test]
        public void GetAllOrders_ReturnOrderList_Successful()
        {
            OrderServiceMock.Setup(order => order.GetOrders()).Returns(GetOrders());
            var query = new GetAllOrdersQueryHandler(OrderServiceMock.Object);

            var response = query.Handle(new GetAllOrdersQuery(), default);
            var result = response.Result.Data;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<OrderVm>>(result);
        }

        [Test]
        public void GetAllOrders_ReturnOrderList_Fail()
        {
            OrderServiceMock.Setup(order => order.GetOrders()).Returns(GetEmptyOrders());
            var query = new GetAllOrdersQueryHandler(OrderServiceMock.Object);

            var response = query.Handle(new GetAllOrdersQuery(), default);
            var result = response.Result.Data;

            Assert.IsNotNull(result);
        }
    }
}
