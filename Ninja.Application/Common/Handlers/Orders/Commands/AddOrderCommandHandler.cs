using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Orders.Commands;
using Ninja.Domain.Entities.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Orders.Commands
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Response<OrderVm>>
    {
        private readonly IOrderService _orderService;

        public AddOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<Response<OrderVm>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new CreateOrder { 
                 Products= request.Products.Select(p => new ProductOrder { ProductId = p.ProductId, Price = p.Price, Quantity = p.Quantity }).ToList(), 
                UserId = request.UserId };
            var newOrder = await _orderService.CreateOrder(order);

            if (newOrder == default)
                return Response.Fail500ServiceError<OrderVm>("Order Service Fail");

            return Response.Ok200(new OrderVm { OrderId = newOrder, ProductOrders = request.Products, UserId = request.UserId });
        }
    }
}
