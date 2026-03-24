using System.ComponentModel.DataAnnotations;
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


	
		//private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		//{
		//	context.Response.ContentType = "application/json";

		//	// Default to 500
		//	context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		//	var message = "An unexpected error occurred.";

		//	// Map your folder's exceptions to HTTP Status Codes
		//	if (exception is NotFoundException)
		//	{
		//		context.Response.StatusCode = StatusCodes.Status404NotFound;
		//		message = exception.Message;
		//	}
		//	else if (exception is ValidationException)
		//	{
		//		context.Response.StatusCode = StatusCodes.Status400BadRequest;
		//		message = exception.Message;
		//	}

		//	return context.Response.WriteAsync(JsonSerializer.Serialize(new
		//	{
		//		status = context.Response.StatusCode,
		//		error = message
		//	}));
		//}




	}

}
