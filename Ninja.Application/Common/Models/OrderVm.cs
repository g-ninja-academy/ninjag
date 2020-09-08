using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common.Models
{
    public class OrderVm
    {
        public Guid OrderId { get; set; }
        public List<ProductOrderVm> ProductOrders { get; set; }
        public Guid UserId { get; set; }
    }
}
