using Moq;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.ProductModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Infrastructure.UnitTests.ServicesTests.ProductServiceTests
{
    [TestFixture]
    public class GetAllProductsTests
    {
        private Mock<IProductService> _serviceMock;
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IProductService>();
        }
        [Test]
        public async Task GetAllProducts_Successfully()
        {
            _serviceMock.Setup(x => x.GetProducts()).ReturnsAsync(new List<Product>().AsEnumerable());

            var result = await _serviceMock.Object.GetProducts();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Product>>(result);
        }
    }
}
