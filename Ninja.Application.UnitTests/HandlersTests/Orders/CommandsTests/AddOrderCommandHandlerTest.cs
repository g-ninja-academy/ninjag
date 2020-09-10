using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Orders.Commands;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Orders.Commands;
using Ninja.Domain.Entities.OrderModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.HandlersTests.Orders.CommandsTests
{
    [TestFixture]
    public class AddOrderCommandHandlerTest
    {
        public Mock<IOrderService> OrderServiceMock { get; set; }

        [SetUp]
        public void Setup()
        {
            OrderServiceMock = new Mock<IOrderService>();
        }

        private async Task<Guid> GetGuid() => new Guid("6fca31da-78ec-4a75-ba0a-bd73363a16d2");
        
        private async Task<Guid> GetEmptyGuid() => new Guid();

        private List<ProductOrderVm> GetProductOrders() => new List<ProductOrderVm>() { 
            new ProductOrderVm() { Price = 10, ProductId = Guid.NewGuid(), Quantity = 2}
        };

        [Test]
        public void AddOrder_ReturnOrderVm_Successfully()
        {
            OrderServiceMock.Setup(order => order.CreateOrder(It.IsAny<CreateOrder>())).Returns(GetGuid());
            var command = new AddOrderCommandHandler(OrderServiceMock.Object);

            var response = command.Handle(new AddOrderCommand(Guid.NewGuid(), GetProductOrders()), default);
            var result = response.Result.Data;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OrderVm>(result);
        }

        [Test]
        public void AddOrder_ReturnOrderVm_Fail()
        {
            OrderServiceMock.Setup(order => order.CreateOrder(It.IsAny<CreateOrder>())).Returns(GetEmptyGuid());

            var command = new AddOrderCommandHandler(OrderServiceMock.Object);
            var response = command.Handle(new AddOrderCommand(Guid.NewGuid(), GetProductOrders()), default);
            var result = response.Result;

            Assert.IsFalse(result.Success);
            Assert.IsInstanceOf<Response<OrderVm>>(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
