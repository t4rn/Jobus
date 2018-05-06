using Jobus.Core.Services.WsClients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Jobus.Api.Middleware
{
    public class HashAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HashAuthorizationMiddleware> _logger;
        private readonly IWsClientService _wsClientService;

        public HashAuthorizationMiddleware(RequestDelegate next, ILogger<HashAuthorizationMiddleware> logger,
            IWsClientService wsClientService)
        {
            _next = next;
            _logger = logger;
            _wsClientService = wsClientService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                string hashFromHeader = context.Request.Headers["Hash"];
                if (string.IsNullOrWhiteSpace(hashFromHeader))
                {
                    // no authorization header
                    _logger.LogDebug($"No hash provided...");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                else
                {
                    // validating hash
                    bool isHashCorrect = await _wsClientService.IsHashCorrectAsync(hashFromHeader);

                    if (isHashCorrect)
                    {
                        await _next(context);
                    }
                    else
                    {
                        // bad hash
                        _logger.LogDebug($"Auth failed for hash: '{hashFromHeader}'");
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[AuthMiddleware] Ex: {ex}");
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;

                return;
            }
        }
    }

}
