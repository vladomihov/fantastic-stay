using System;
using System.Net;
using FantasticStay.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FantasticStay.API.Infrastructure.Filters
{
	public class HttpGlobalExceptionFilter : IExceptionFilter
	{
		private readonly IWebHostEnvironment _environment;
		private readonly ILogger<HttpGlobalExceptionFilter> _logger;

		public HttpGlobalExceptionFilter(IWebHostEnvironment environment, ILogger<HttpGlobalExceptionFilter> logger)
		{
			_environment = environment ?? throw new ArgumentNullException(nameof(environment));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public void OnException(ExceptionContext context)
		{
			_logger.LogError(new EventId(context.Exception.HResult),
				context.Exception,
				context.Exception.Message);

			if (context.Exception.GetType() == typeof(FantasticStayDomainException))
			{
				var problemDetails = new ValidationProblemDetails()
				{
					Instance = context.HttpContext.Request.Path,
					Status = StatusCodes.Status400BadRequest,
					Detail = "Please refer to the errors property for additional details."
				};

				problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

				context.Result = new BadRequestObjectResult(problemDetails);
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
			else
			{
				var json = new JsonErrorResponse
				{
					Messages = new[] { "An error occur.Try it again." }
				};

				if (_environment.IsDevelopment())
				{
					json.DeveloperMessage = context.Exception;
				}

				context.Result = new ObjectResult(json) { StatusCode = StatusCodes.Status500InternalServerError };
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}
			context.ExceptionHandled = true;
		}

		private class JsonErrorResponse
		{
			public string[] Messages { get; set; }

			public object DeveloperMessage { get; set; }
		}
	}
}
