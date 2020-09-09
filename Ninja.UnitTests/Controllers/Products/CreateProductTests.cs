using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ninja.Api.Controllers;
using Ninja.Application.Common;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Commands;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Ninja.Api.UnitTests.Controllers.Products
{
    [TestFixture]
    public class CreateProductTests
    {
        private Mock<IMediator> _mediator;
        private ProductController _controller;

        private static IEnumerable<ProductVm> GetProduct()
        {
            var result = new List<ProductVm>();

            result.Add(new ProductVm()
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Price = 34.998m
            });
            return result;
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new ProductController(_mediator.Object);
        }

        [Test, TestCaseSource("GetProduct")]
        public void CreateProductSuccessfully(ProductVm product)
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddProductCommand>(), default)).ReturnsAsync(Response.Ok200<ProductVm>(product));

            var result = _controller.Post(product);
            var objectResult = (ObjectResult)result.Result.Result;
            var prod = (Response<ProductVm>)objectResult.Value;

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.IsInstanceOf<Response<ProductVm>>(prod);
            Assert.AreEqual(product.Id, prod.Data.Id);
        }

        [Test, TestCaseSource("GetProduct")]
        public void CreateUser_ThowsException(ProductVm product)
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddProductCommand>(), default))
                .Throws(new Exception());

            Assert.That(() => _controller.Post(product), Throws.TypeOf<Exception>());
        }
    }
}
