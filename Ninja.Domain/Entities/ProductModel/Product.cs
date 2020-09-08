using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Domain.Entities.ProductModel
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
