using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Domain.Entities.OrderModel
{
    public class ProductOrder
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
