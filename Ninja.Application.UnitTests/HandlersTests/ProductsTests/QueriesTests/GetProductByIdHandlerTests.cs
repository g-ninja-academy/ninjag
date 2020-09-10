using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Products.Queries;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Queries;
using Ninja.Domain.Entities.ProductModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.HandlersTests.ProductsTests.QueriesTests
{
    [TestFixture]
    public class GetProductByIdHandlerTests
    {
        private Mock<IProductService> _productService;

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
            _productService = new Mock<IProductService>();
        }

        [Test, TestCaseSource("GetProduct")]
        public void GetProductByIdSuccessfully(ProductVm product)
        {
            _productService.Setup(ps => ps.GetProductById(It.IsAny<Guid>())).ReturnsAsync(new Product { Id = product.Id, Name = product.Name, Price=product.Price});
            var handler = new GetProductByIdQueryHandler(_productService.Object);

            var result = handler.Handle(new GetProductByIdQuery(product.Id), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<ProductVm>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }

        [Test, TestCaseSource("GetProduct")]
        public void GetProdcuctByIdNotFound(ProductVm product)
        {
            var handler = new GetProductByIdQueryHandler(_productService.Object);

            var result = handler.Handle(new GetProductByIdQuery(product.Id), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<ProductVm>>>(result);
            Assert.AreEqual(StatusCodes.Status404NotFound, result.Result.StatusCode);
        }
    }
}
