using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;

namespace Ninja.Application.Products.Commands
{
    public class AddProductCommand : IRequest<Response<ProductVm>>
    {
        public ProductVm Product { get; set; }
    }
}
