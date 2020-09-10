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
    public class GetAllOrdersTest
    {
        private Mock<IMediator> _mediator;
        private OrderController _orderController;
        private IEnumerable<OrderVm> _orderList;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _orderController = new OrderController(_mediator.Object);
            _orderList = new List<OrderVm>()
            {
                new OrderVm() { 
                    OrderId = Guid.NewGuid(), 
                    UserId = Guid.NewGuid(), 
                    ProductOrders = new List<ProductOrderVm>()
                    {
                        new ProductOrderVm() { ProductId = Guid.NewGuid(), Price = 10, Quantity = 2 }
                    }
                }
            };
        }

        [Test]
        public void GetAllOrders_ReturnOrderList_Successful()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetAllOrdersQuery>(), default)).ReturnsAsync(Response.Ok200(_orderList));

            var response = _orderController.Get();
            var result = (ObjectResult)response.Result.Result;
            var orderList = (Response<IEnumerable<OrderVm>>)result.Value;

            Assert.IsNotNull(orderList);
            Assert.AreEqual(StatusCodes.Status200OK, orderList.StatusCode);
            Assert.AreEqual(((List<OrderVm>)orderList.Data).Count, 1);
        }

        [Test]
        public void GetAllOrders_ReturnOrderList_Fail()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetAllOrdersQuery>(), default))
                .Throws(new Exception());

            Assert.That(() => _orderController.Get(), Throws.TypeOf<Exception>());
        }

    }
}
