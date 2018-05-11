using Jobus.Core.Services.WsClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Jobus.Api.Middleware
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ILogger<AuthorizationFilter> _logger;
        private readonly IWsClientService _wsClientService;

        public AuthorizationFilter(ILogger<AuthorizationFilter> logger,
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
                    bool isHashCorrect = await _wsClientService.IsHashCorrectAsync(hashFromHeader);

                    if (!isHashCorrect)
                    {
                        // bad hash
                        _logger.LogDebug($"Auth failed for hash: '{hashFromHeader}'");
                        context.Result = new ContentResult()
                        {
                            Content = $"Hash '{hashFromHeader}' is unauthorized for Action '{actionName}' in Controller '{controllerName}'.",
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
