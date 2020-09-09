using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common.Models
{
    public class ProductVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
