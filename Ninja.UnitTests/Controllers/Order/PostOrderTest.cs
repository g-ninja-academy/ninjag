using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Orders.Commands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Api.UnitTests.Controllers.Order
{
    [TestFixture]
    public class PostOrderTest
    {
        private Mock<IMediator> _mediator;
        private OrderController _orderController;
        private List<ProductOrderVm> _productOrderVms;
        private OrderVm _orderVm;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _orderController = new OrderController(_mediator.Object);
            _productOrderVms = new List<ProductOrderVm>()
            {
                new ProductOrderVm() { ProductId = Guid.NewGuid(), Price = 10, Quantity = 2 }
            };
            _orderVm = new OrderVm() { OrderId = Guid.NewGuid(), ProductOrders = _productOrderVms, UserId = Guid.NewGuid() };
        }

        [Test]
        public void PostOrder_ReturnOrder_Successful()
        {
            _mediator.Setup(x => x.Send(It.IsAny<AddOrderCommand>(), default)).ReturnsAsync(
                Response.Ok200(
                    new OrderVm() { OrderId = Guid.NewGuid(), UserId = Guid.NewGuid(), ProductOrders = _productOrderVms }
                )
            );

            var response = _orderController.Post(_orderVm);
            var result = (ObjectResult)response.Result.Result;
            var order = (Response<OrderVm>)result.Value;

            Assert.IsNotNull(order);
            Assert.AreEqual(StatusCodes.Status200OK, order.StatusCode);
        }

        [Test]
        public void PostOrder_ReturnOrder_Fail()
        {
            _mediator.Setup(x => x.Send(It.IsAny<AddOrderCommand>(), default))
                .Throws(new Exception());

            Assert.That(() => _orderController.Post(_orderVm), Throws.TypeOf<Exception>());
        }
    }
}
