using Microsoft.Extensions.Options;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.OrderModel;
using Ninja.Infrastructure.Services.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ninja.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderSettings _orderSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient HttpClient;
        public OrderService(IHttpClientFactory httpClientFactory, IOptions<OrderSettings> options)
        {
            _orderSettings = options.Value;
            _httpClientFactory = httpClientFactory;
            HttpClient = _httpClientFactory.CreateClient();
        }

        public async Task<Guid> CreateOrder(CreateOrder createOrder)
        {
            var httpMessage = HttpUtils.GetRequestMessage(
                 $"{_orderSettings.Url}{_orderSettings.CreateOrder}",
                 HttpMethod.Post,
                 new StringContent(JsonSerializer.Serialize(createOrder), Encoding.UTF8, MediaTypeNames.Application.Json)
                 );

            var result = await HttpClient.SendAsync(httpMessage);
            return await HttpUtils.MapHttpResponse<Guid>(result);
        }

        public async Task<Order> GetOrderById(Guid OrderId)
        {
            var httpMessage = HttpUtils.GetRequestMessage(
               $"{_orderSettings.Url}{_orderSettings.GetOrderById}",
               HttpMethod.Get
               );
            var result = await HttpClient.SendAsync(httpMessage);
            return await HttpUtils.MapHttpResponse<Order>(result);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var httpMessage = HttpUtils.GetRequestMessage(
               $"{_orderSettings.Url}{_orderSettings.GetOrders}",
               HttpMethod.Get
               );
            var result = await HttpClient.SendAsync(httpMessage);
            return await HttpUtils.MapHttpResponse<IEnumerable<Order>>(result);
        }
    }
}
