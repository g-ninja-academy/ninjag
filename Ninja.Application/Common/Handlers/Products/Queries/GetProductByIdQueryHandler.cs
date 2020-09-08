using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Products.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductVm>>
    {
        private readonly IProductService _productService;

        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Response<ProductVm>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _productService.GetProductById(request.Id);

            if (response == null)
                return Response.Fail404NotFound<ProductVm>("Product Not Found");

            return Response.Ok200(new ProductVm() { Id = response.Id, Name = response.Name, Price = response.Price });
        }
    }
}
