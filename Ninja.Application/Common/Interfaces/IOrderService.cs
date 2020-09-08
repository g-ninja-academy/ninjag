using Ninja.Domain.Entities.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrder(CreateOrder createOrder);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(Guid orderId);
    }
}
