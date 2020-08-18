using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ninja.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException( HttpContext context, Exception exception)
        {
            string message = "";

            switch(exception)
            {
                case Exception e:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = e.Message;
                    break;
                default:
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(new { message = message }));
        }

    }
}
