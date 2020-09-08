using Ninja.Domain.Entities.ProductModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common.Interfaces
{
    public interface IProductService
    {
        Guid CreateProduct(Product product);
        IEnumerable<Product> GetProducts();
        Product GetProductById(Guid productId);
    }
}
