using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        public Guid CreateOrder(IEnumerable<ProductOrder> productOrders)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(Guid OrderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
