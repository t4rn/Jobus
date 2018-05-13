using Jobus.Common.Results;
using Jobus.Core.Services.WsClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Jobus.Api.Filters
{
    public class HashAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ILogger<HashAuthorizationFilter> _logger;
        private readonly IWsClientService _wsClientService;

        public HashAuthorizationFilter(ILogger<HashAuthorizationFilter> logger,
            IWsClientService wsClientService)
        {
            _logger = logger;
            _wsClientService = wsClientService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                string controllerName = controllerActionDescriptor?.ControllerName;
                string actionName = controllerActionDescriptor?.ActionName;

                string hashFromHeader = context.HttpContext.Request.Headers["Hash"];
                if (string.IsNullOrWhiteSpace(hashFromHeader))
                {
                    // no authorization header
                    _logger.LogDebug($"No hash provided...");
                    context.Result = new ContentResult()
                    {
                        Content = $"No Hash provided.",
                        StatusCode = StatusCodes.Status401Unauthorized,
                    };
                }
                else
                {
                    // validating hash
                    Result validationResult = await _wsClientService.AuthorizeAsync(hashFromHeader, controllerName, actionName);

                    if (!validationResult.IsOk)
                    {
                        _logger.LogDebug($"Auth failed -> {validationResult.Message}");
                        context.Result = new ContentResult()
                        {
                            Content = validationResult.Message,
                            StatusCode = StatusCodes.Status401Unauthorized,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[OnAuthorizationAsync] Ex: {ex}");
                context.Result = new ContentResult()
                {
                    Content = $"Exception occured: {ex.GetBaseException().Message}.",
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                };
            }
        }
    }
}
