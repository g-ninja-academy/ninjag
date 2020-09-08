using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Domain.Entities.OrderModel
{
    public class CreateOrder
    {
        public List<ProductOrder> Products { get; set; }
        public Guid UserId { get; set; }

    }
}
