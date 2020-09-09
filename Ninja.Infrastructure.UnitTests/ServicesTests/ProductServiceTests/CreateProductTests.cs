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
    public class CreateProductTests
    {
        private Mock<IProductService> _serviceMock;
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IProductService>();
        }
        [Test]
        [TestCase("product1",1.22)]
        public async Task CreateProduct_Successfully(string productName, decimal price)
        {
            _serviceMock.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(Guid.NewGuid());

            var result = await _serviceMock.Object.CreateProduct(new Product { Name = productName, Price = price });

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Guid>(result);
        }
        [Test]
        [TestCase("product1", 1.22)]
        public void CreateProduct_throwException(string productName, decimal price)
        {
            _serviceMock.Setup(x => x.CreateProduct(It.IsAny<Product>())).Throws(new Exception());

            Assert.That(() => _serviceMock.Object.CreateProduct(new Product { Name = productName, Price = price }), Throws.TypeOf<Exception>());
        }
    }
}
