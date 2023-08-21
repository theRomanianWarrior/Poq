using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PoqAssignment.Domain.Exceptions;

namespace PoqAssignment.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
                throw;
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = exception switch
            {
                JsonException _ => (int) HttpStatusCode.BadRequest,
                InvalidOperationException _ => (int) HttpStatusCode.BadRequest,
                LoadAllMockyProductsFailedException _ => (int) HttpStatusCode.BadRequest,
                _ => (int) HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new {message = exception.Message});

            await response.WriteAsync(result);
        }
    }
}