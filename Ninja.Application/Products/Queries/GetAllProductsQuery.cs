using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<Response<IEnumerable<ProductVm>>>
    {
    }
}
