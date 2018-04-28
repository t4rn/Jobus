using Jobus.Core.Services.WsClients;
using Jobus.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Jobus.Api.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected readonly string _ip;
        private readonly IWsClientService _wsClientService;

        public AbstractController(IActionContextAccessor actionContextAccessor,
            IWsClientService wsClientService)
        {
            _ip = actionContextAccessor?.ActionContext?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _wsClientService = wsClientService;
        }

        /// <summary>
        /// Returns a ObjectResult with 500 code
        /// </summary>
        protected ObjectResult InternalServerError(object obj)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, obj);
        }

        protected string LogDescription([CallerMemberName] string methodName = null)
        {
            return $"[{methodName} - {_ip}]";
        }

        protected async Task<WsClient> GetWsClient()
        {
            var hash = Request.Headers["Hash"];

            IEnumerable<WsClient> wsClients = await _wsClientService.GetWsClientsAsync();

            return wsClients.FirstOrDefault(x => x.Hash == hash);
        }
    }
}
