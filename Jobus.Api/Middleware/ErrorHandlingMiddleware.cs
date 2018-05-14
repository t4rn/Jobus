using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Jobus.Api.Middleware
{
    /// <summary>
    /// Global exception handler
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> log)
        {
            // TODO: move to ExceptionFilter?
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                log.LogError($"Global Ex: {ex}");

                HttpStatusCode code = HttpStatusCode.ServiceUnavailable;

                context.Response.ContentType = "text/plain";// "application/json";
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(ex.GetBaseException().Message);
            }
        }
    }
}
