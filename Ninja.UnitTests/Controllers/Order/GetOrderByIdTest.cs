using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Orders.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Api.UnitTests.Controllers.Order
{
    [TestFixture]
    public class GetOrderByIdTest
    {
        private Mock<IMediator> _mediator;
        private OrderController _orderController;
        private OrderVm _order;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _orderController = new OrderController(_mediator.Object);
            _order = new OrderVm()
            {
                OrderId = new Guid("6fca31da-78ec-4a75-ba0a-bd73363a16d2"),
                UserId = Guid.NewGuid(),
                ProductOrders = new List<ProductOrderVm>()
                {
                    new ProductOrderVm() { ProductId = Guid.NewGuid(), Price = 10, Quantity = 2 }
                }
            };
        }

        [Test]
        [TestCase("6fca31da-78ec-4a75-ba0a-bd73363a16d2")]
        public void GetOrderById_ReturnOrder_Successful(Guid Id)
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetOrderByIdQuery>(), default)).ReturnsAsync(Response.Ok200(_order));

            var response = _orderController.Get(Id);
            var result = (ObjectResult)response.Result.Result;
            var order = (Response<OrderVm>)result.Value;

            Assert.IsNotNull(order);
            Assert.AreEqual(StatusCodes.Status200OK, order.StatusCode);
            Assert.AreEqual(order.Data.OrderId, Id);
        }

        [Test]
        [TestCase("6fca31da-78ec-4a75-ba0a-bd73363a16d2")]
        public void GetOrderById_ReturnOrder_Fail(Guid Id)
        {
            _order.OrderId = new Guid("6fca31da-78ec-4a75-ba0a-bd73363a16d3");
            _mediator.Setup(x => x.Send(It.IsAny<GetOrderByIdQuery>(), default)).ReturnsAsync(Response.Fail404NotFound<OrderVm>(""));

            var response = _orderController.Get(Id);
            var result = (ObjectResult)response.Result.Result;
            var order = (Response<OrderVm>)result.Value;

            Assert.IsNull(order.Data);
            Assert.AreEqual(StatusCodes.Status404NotFound, order.StatusCode);
        }

    }
}
