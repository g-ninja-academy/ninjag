using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Products.Commands;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Commands;
using Ninja.Domain.Entities.ProductModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.HandlersTests.ProductsTests.CommandsTests
{
    [TestFixture]
    public class CreateProductHandlerTests
    {
        private Mock<IProductService> _productService;

        private static IEnumerable<Product> GetProduct()
        {
            var result = new List<Product>();

            result.Add(new Product()
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

        [Test,TestCaseSource("GetProduct")]
        public void CreateProductSuccessfully(Product product) 
        {
            _productService.Setup(ps => ps.CreateProduct(It.IsAny<Product>())).ReturnsAsync(product.Id);

            var handler = new AddProductCommandHandler(_productService.Object);

            var result = handler.Handle(new AddProductCommand()
            {
                 Product = new ProductVm()
                {
                    Name = product.Name,
                    Price = product.Price
                }
            }, default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<ProductVm>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }

    }
}
