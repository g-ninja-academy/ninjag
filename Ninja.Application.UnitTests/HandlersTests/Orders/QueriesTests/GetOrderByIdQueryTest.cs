using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Orders.Queries;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
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
    public class GetOrderByIdQueryTest
    {
        public Mock<IOrderService> OrderServiceMock { get; set; }

        [SetUp]
        public void Setup()
        {
            OrderServiceMock = new Mock<IOrderService>();
        }

        private async Task<Order> GetOrder()
        {
            var products = new List<ProductOrder>() { new ProductOrder() { ProductId = Guid.NewGuid(), Price = 19, Quantity = 2 } };
            return new Order() { OrderId = Guid.NewGuid(), UserId = Guid.NewGuid(), Products = products };
        }

        private async Task<Order> GetEmptyOrder() => new Order() { Products = new List<ProductOrder>() };

        [Test]
        public void GetOrderById_ReturnOrder_Successful()
        {
            OrderServiceMock.Setup(order => order.GetOrderById(It.IsAny<Guid>())).Returns(GetOrder());
            var query = new GetOrderByIdQueryHandler(OrderServiceMock.Object);

            var response = query.Handle(new GetOrderByIdQuery(Guid.NewGuid()), default);
            var result = response.Result.Data;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OrderVm>(result);
            Assert.AreEqual(1, result.ProductOrders.Count);
        }

        [Test]
        public void GetOrderById_ReturnOrder_Fail404NotFound()
        {
            OrderServiceMock.Setup(order => order.GetOrderById(It.IsAny<Guid>()));
            var query = new GetOrderByIdQueryHandler(OrderServiceMock.Object);

            var response = query.Handle(new GetOrderByIdQuery(Guid.NewGuid()), default);
            var result = response.Result;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Response<OrderVm>>(result);
        }
    }
}
