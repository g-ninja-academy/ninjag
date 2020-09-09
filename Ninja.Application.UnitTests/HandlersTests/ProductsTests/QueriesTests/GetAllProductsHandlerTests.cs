using Microsoft.AspNetCore.Http;
using Moq;
using Ninja.Application.Common;
using Ninja.Application.Common.Handlers.Products.Queries;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Common.Models;
using Ninja.Application.Products.Queries;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ninja.Application.UnitTests.HandlersTests.ProductsTests.QueriesTests
{
    [TestFixture]
    public class GetAllProductsHandlerTests
    {
        private Mock<IProductService> _productService;


        [SetUp]
        public void SetUp()
        {
            _productService = new Mock<IProductService>();
        }

        [Test]
        public void GetAllProductsSuccessfully()
        {
            var handler = new GetAllProductsQueryHandler(_productService.Object);

            var result = handler.Handle(new GetAllProductsQuery(), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Response<IEnumerable<ProductVm>>>>(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.Result.StatusCode);
        }
    }
}
