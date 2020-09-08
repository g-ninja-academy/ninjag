using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using System;

namespace Ninja.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Response<ProductVm>>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
