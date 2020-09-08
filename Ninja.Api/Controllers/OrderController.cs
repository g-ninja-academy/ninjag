using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Orders.Commands;
using Ninja.Application.Orders.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ninja.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get All Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<List<OrderVm>>>> Get()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get by Order Id 
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderVm>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Create a new Order
        /// </summary>
        /// <param name="value">Order object to be created</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderVm>> Post([FromBody] OrderVm value)
        {
            var result = await _mediator.Send(new AddOrderCommand(value.UserId, value.ProductOrders));
            return StatusCode(result.StatusCode, result);
        }
    }
}
