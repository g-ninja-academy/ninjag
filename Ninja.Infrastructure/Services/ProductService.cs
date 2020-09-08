using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.ProductModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        public Guid CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
