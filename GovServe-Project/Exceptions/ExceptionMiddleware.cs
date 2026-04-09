using GovServe_Project.Exceptions;
using System.Net;
using System.Text.Json;
public class ExceptionMiddleware

{

	private readonly RequestDelegate _next;



	public ExceptionMiddleware(RequestDelegate next)

	{

		_next = next;

	}



	public async Task InvokeAsync(HttpContext context)

	{

		try

		{

			await _next(context);

		}

		catch (Exception ex)

		{

			await HandleExceptionAsync(context, ex);

		}

	}



	private Task HandleExceptionAsync(HttpContext context, Exception ex)

	{

		HttpStatusCode statusCode;

		string message = ex.Message;



		switch (ex)

		{

			case NotFoundException:

				statusCode = HttpStatusCode.NotFound; // 404

				break;



			case BadRequestException:

				statusCode = HttpStatusCode.BadRequest; // 400

				break;



			default:

				statusCode = HttpStatusCode.InternalServerError; // 500

				message = "Internal Server Error";

				break;

		}



		var result = JsonSerializer.Serialize(new

		{

			StatusCode = (int)statusCode,

			Message = message

		});



		context.Response.ContentType = "application/json";

		context.Response.StatusCode = (int)statusCode;



		return context.Response.WriteAsync(result);

	}

}