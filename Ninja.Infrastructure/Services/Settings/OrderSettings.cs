using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Infrastructure.Services.Settings
{
    public class OrderSettings
    {
        public string Url { get; set; }
        public string CreateOrder { get; set; }
        public string GetOrders { get; set; }
        public string GetOrderById { get; set; }
    }
}
