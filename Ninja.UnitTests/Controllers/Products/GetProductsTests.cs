using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ninja.Api.UnitTests.Controllers.Products
{
    public class GetProductsTests
    {
        private Mock<IMediator> _mediator;
        private ProductController _controller;

        private static IEnumerable<IEnumerable<ProductVm>> GetProduct()
        {
            var result = new List<ProductVm>();

            result.Add(new ProductVm()
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Price = 34.998m
            });

            var collection = new List<List<ProductVm>>();
            collection.Add(result);
            return collection;
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new ProductController(_mediator.Object);
        }

        [Test, TestCaseSource("GetProduct")]
        public void GetProductByIdSuccessfully(IEnumerable<ProductVm> products)
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), default)).ReturnsAsync(Response.Ok200<IEnumerable<ProductVm>>(products));

            var result = _controller.Get();
            var objectResult = (ObjectResult)result.Result.Result;
            var prods = (Response<IEnumerable<ProductVm>>)objectResult.Value;

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Response<IEnumerable<ProductVm>>>(prods);
            Assert.AreEqual(1, prods.Data.Count());
        }

        [Test]
        public void CreateUser_ThowsException()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), default))
                .Throws(new Exception());

            Assert.That(() => _controller.Get(), Throws.TypeOf<Exception>());
        }
    }
}
