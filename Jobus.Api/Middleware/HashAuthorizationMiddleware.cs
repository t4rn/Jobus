using Jobus.Common.Results;
using Jobus.Core.Services.WsClients;
using Jobus.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                else
                {
                    // validating hash
                    Result validationResult = await ValidateHash(hashFromHeader);

                    if (validationResult.IsOk)
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

        private async Task<Result> ValidateHash(string hashFromHeader)
        {
            Result result = new Result();

            IEnumerable<WsClient> wsClients = await _wsClientService.GetWsClientsAsync();

            if (wsClients.Any(x => x.Hash == hashFromHeader))
                result.IsOk = true;

            return result;
        }
    }

}
