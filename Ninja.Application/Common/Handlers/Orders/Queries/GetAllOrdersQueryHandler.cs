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
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Response<IEnumerable<OrderVm>>>
    {
        private readonly IOrderService _orderService;
        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<Response<IEnumerable<OrderVm>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var response = await _orderService.GetOrders();

            var result = response.Select(o =>
                new OrderVm { 
                    OrderId = o.OrderId, 
                    UserId = o.UserId, 
                    ProductOrders = o.Products.Select(p =>
                        new ProductOrderVm { 
                            Price = p.Price, 
                            ProductId = p.ProductId, 
                            Quantity = p.Quantity }).ToList() 
                });

            return Response.Ok200(result);
            
        }
    }
}
