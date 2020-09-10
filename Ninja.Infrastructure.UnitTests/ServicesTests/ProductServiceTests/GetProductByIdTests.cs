using Moq;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.ProductModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Infrastructure.UnitTests.ServicesTests.ProductServiceTests
{
    [TestFixture]
    public class GetProductByIdTests
    {
        private Mock<IProductService> _serviceMock;
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IProductService>();
        }
        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6")]
        public async Task GetProductById_Successsfully(string ProductId)
        {
            _serviceMock.Setup(x => x.GetProductById(It.IsAny<Guid>())).ReturnsAsync(new Product());

            var result = await _serviceMock.Object.GetProductById(new Guid(ProductId));


            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Product>(result);
        }
        [Test]
        [TestCase("f5d958ec-d760-4abe-bf3e-c8ba12c975e6")]
        public async Task GetProductById_NotFound(string ProductId)
        {
            _serviceMock.Setup(x => x.GetProductById(It.IsAny<Guid>()));

            var result = await _serviceMock.Object.GetProductById(new Guid(ProductId));


            Assert.IsNull(result);
        }
    }
}
