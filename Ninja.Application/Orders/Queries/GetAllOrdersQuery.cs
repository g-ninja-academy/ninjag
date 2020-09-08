using MediatR;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<Response<IEnumerable<OrderVm>>>
    {
    }
}
