using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode = (exception is InvalidOperationException) ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.InternalServerError;

            context.Response.StatusCode = statusCode;

            var response = new
            {
                StatusCode = statusCode,
                Message = exception.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}