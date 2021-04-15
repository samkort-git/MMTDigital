using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MMTShop.Extensions
{
    /// <summary>
    /// Error Handler (Middleware)
    /// </summary>
    public sealed class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            Next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Invoke method (calling the next middleware).
        /// </summary>
        /// <param name="context">Http Context</param>
        /// <returns>Request Delegate</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                await HandledExceptionAsync(context, ex);
            }
        }

        private static Task HandledExceptionAsync(HttpContext context, System.Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = JsonConvert.SerializeObject(new { messages = new string[] { exception.Message } });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
