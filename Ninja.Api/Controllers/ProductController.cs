using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Commands;
using Ninja.Application.Products.Queries;

namespace Ninja.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<List<ProductVm>>>> Get()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductVm>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductVm>> Post([FromBody] ProductVm value)
        {
            var result = await _mediator.Send(new AddProductCommand() { Product = value});
            return StatusCode(result.StatusCode, result);
        }
    }
}
