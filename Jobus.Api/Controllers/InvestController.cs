using Jobus.Core.Services.WsClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jobus.Api.Controllers
{
    [Route("api/[controller]")]
    public class InvestController : AbstractController
    {
        private readonly ILogger _logger;

        public InvestController(ILogger<InvestController> logger, IActionContextAccessor actionContextAccessor,
            IWsClientService wsClientService)
            : base(actionContextAccessor, wsClientService)
        {
            _logger = logger;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Ping()
        {
            _logger.LogDebug($"{LogDescription()} Ping START");
            return Ok($"Ping at '{DateTime.Now}' from '{_ip}'.");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public string Get(int id)
        {

            // TODO: authorize WsClient vs controller method
            // TODO: log errors to DB
            return $"You've sent value = '{id}.";
        }
    }
}
