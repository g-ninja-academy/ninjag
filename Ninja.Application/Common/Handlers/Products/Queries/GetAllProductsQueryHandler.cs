using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Ninja.Application.Common.Handlers.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<IEnumerable<ProductVm>>>
    {
        private readonly IProductService _productService;

        public GetAllProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Response<IEnumerable<ProductVm>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var response = await _productService.GetProducts();

            var result = response.Select(prod => new ProductVm()
            {
                Id = prod.Id,
                Name = prod.Name,
                Price = prod.Price            
            });

            return Response.Ok200(result);
        }
    }
}
