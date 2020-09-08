using Ninja.Domain.Entities.ProductModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(Guid productId);
    }
}
