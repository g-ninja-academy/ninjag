using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<Response<OrderVm>>
    {
        public Guid OrderId { get; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
