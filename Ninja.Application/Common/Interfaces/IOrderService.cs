using Ninja.Domain.Entities.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common.Interfaces
{
    public interface IOrderService
    {
        Guid CreateOrder(IEnumerable<ProductOrder> productOrders);
        IEnumerable<Order> GetOrders();
        Order GetOrderById(Guid OrderId);
    }
}
