using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Domain.Entities.OrderModel
{
    public class CreateOrder
    {
        public List<ProductOrder> productOrders { get; set; }
        public Guid UserId { get; set; }

    }
}
