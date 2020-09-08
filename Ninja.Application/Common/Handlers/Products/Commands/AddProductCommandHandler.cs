using MediatR;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Commands;
using Ninja.Domain.Entities.ProductModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Handlers.Products.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<ProductVm>>
    {
        private readonly IProductService _productService;

        public AddProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Response<ProductVm>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product() { Name = request.Product.Name, Price = request.Product.Price };
            var result = await _productService.CreateProduct(newProduct);

            if (result == default)
                return Response.Fail500ServiceError<ProductVm>("Product Service Fail");

            return Response.Ok200(new ProductVm() { Id = result, Name = newProduct.Name, Price = newProduct.Price });
        }
    }
}
