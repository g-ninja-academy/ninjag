using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Infrastructure.Services
{
    public static class HttpUtils
    {
        public static async Task<Out> MapHttpResponse<Out>(HttpResponseMessage httpResponseMessage)
        {
            if ((int)httpResponseMessage.StatusCode != StatusCodes.Status200OK)
            {
                return default;
            }
            var responseData = await httpResponseMessage.Content.ReadFromJsonAsync<Out>();
            return responseData;
        }
        public static HttpRequestMessage GetRequestMessage(string url, HttpMethod httpMethod, HttpContent httpContent = null)
        {
            return new HttpRequestMessage()
            {
                Method = httpMethod,
                RequestUri = new Uri(url),
                Content = httpContent
            };
        }
    }
}
