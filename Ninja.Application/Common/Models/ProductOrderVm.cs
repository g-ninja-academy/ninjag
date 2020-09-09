using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common.Models
{
    public class ProductOrderVm
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
