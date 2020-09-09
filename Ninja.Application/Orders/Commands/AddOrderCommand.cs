using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Orders.Commands
{
    public class AddOrderCommand : IRequest<Response<OrderVm>>
    {
        public Guid UserId { get; }
        public List<ProductOrderVm> Products { get; }

        public AddOrderCommand(Guid userId, List<ProductOrderVm> products)
        {
            UserId = userId;
            Products = products;
        }


    }
}
