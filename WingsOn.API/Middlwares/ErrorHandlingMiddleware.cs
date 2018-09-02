using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WingsOn.API.InfoModels;
using WingsOn.BL;

namespace WingsOn.API.Middlwares
{
    public class ErrorHandlingMiddleware
    {
        private const string GENERIC_ERROR = "Oops, something went wrong. Our specialists are working on fixing this issue.";

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            string result = "";

            if (exception is PersonNotFoundException)
            {
                _logger.LogWarning(exception, exception.Message, exception?.InnerException?.Message);
                code = HttpStatusCode.NotFound;
                result = new ErrorInfo { StatusCode = (int)code, Message = exception.Message}.ToString();
            }
            else
            {
                _logger.LogError(exception, exception.Message, exception?.InnerException?.Message);
                result = new ErrorInfo { StatusCode = (int)code, Message = GENERIC_ERROR }.ToString();
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
