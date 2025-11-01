using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CMSAPI.Middlewares
{
	public class ModelBindingErrorMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ModelBindingErrorMiddleware> _logger;
		public ModelBindingErrorMiddleware(
			RequestDelegate next,
			ILogger<ModelBindingErrorMiddleware> logger)
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
			catch (JsonException ex)
			{
				_logger.LogWarning(ex , "JSON building error");
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				var message = $"درخواست وارد شده با فرمت اشتباه ارسال شده است. برای بخش `{ex.Path}`: {ex.Message}";
				var errorObj = new
				{
					status = 400 ,
					error = message
				};

				await context.Response.WriteAsJsonAsync(errorObj);
				return;
			}
		}
	}
}
