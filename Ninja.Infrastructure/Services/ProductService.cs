using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.ProductModel;
using Ninja.Infrastructure.Services.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ninja.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductSettings _productSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient HttpClient;
        public ProductService(IHttpClientFactory httpClientFactory, IOptions<ProductSettings> options)
        {
            _productSettings = options.Value;
            _httpClientFactory = httpClientFactory;
            HttpClient = _httpClientFactory.CreateClient();
        }
        public async Task<Guid> CreateProduct(Product product)
        {
            var httpMessage = HttpUtils.GetRequestMessage(
                $"{_productSettings.Url}{_productSettings.CreateProduct}",
                HttpMethod.Post,
                new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, MediaTypeNames.Application.Json)
                );

            var result = await HttpClient.SendAsync(httpMessage);
            return await HttpUtils.MapHttpResponse<Guid>(result);

        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var httpMessage = HttpUtils.GetRequestMessage(
               $"{_productSettings.Url}{_productSettings.GetProductById}{productId}",
               HttpMethod.Get
               );
            var result = await HttpClient.SendAsync(httpMessage);
            return await HttpUtils.MapHttpResponse<Product>(result);

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var httpMessage = HttpUtils.GetRequestMessage(
              $"{_productSettings.Url}{_productSettings.GetProducts}",
              HttpMethod.Get
              );

            var result = await HttpClient.SendAsync(httpMessage);
            return await HttpUtils.MapHttpResponse<IEnumerable<Product>>(result);

        }

    }
}
