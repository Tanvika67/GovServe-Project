using System.Net;
using System.Text.Json;
using GovServe_Project.Exceptions;

namespace GovServe_Project.Extensions
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
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await HandleExceptionAsync(context, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(context, "Internal Server Error");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
