using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Orders.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Orders.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderVm>>
    {
        private readonly IOrderService _orderService;
        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Response<OrderVm>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _orderService.GetOrderById(request.OrderId);
            
            if(response == default)
                return Response.Fail404NotFound<OrderVm>("Order was not found");

            var result = new OrderVm
            {
                OrderId = response.OrderId,
                ProductOrders = response.Products.Select(p =>
                   new ProductOrderVm
                   {
                       Price = p.Price,
                       ProductId = p.ProductId,
                       Quantity = p.Quantity
                   }).ToList(),
                UserId = response.UserId
            };

            return Response.Ok200(result);
        }
    }
}
