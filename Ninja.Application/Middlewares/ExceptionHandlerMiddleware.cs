using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ninja.Application.Common.Interfaces;
using FluentValidation;
using System.Linq;

namespace Ninja.Application.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggin _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggin logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            string message = "";
            object data = null;

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    message = validationException.Message;
                    data = validationException.Errors.Select(error => new { error.PropertyName, error.ErrorMessage });
                    _logger.Error(message);
                    break;
                case Exception e:
                    _logger.Error(e.Message);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = e.Message;
                    break;
                default:
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(new { message, data }));
        }
    }
}