using Jobus.Core.Dto;
using Jobus.Core.Services.WsClients;
using Jobus.Domain.WsClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
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
        private ILogger<AbstractController> _log;

        public AbstractController(ILogger<AbstractController> logger, IActionContextAccessor actionContextAccessor,
            IWsClientService wsClientService)
        {
            _log = logger;
            _ip = actionContextAccessor?.ActionContext?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _wsClientService = wsClientService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Ping()
        {
            _log.LogDebug("START for {Ip}", _ip);
            return Ok($"Ping at {DateTime.Now} from {_ip} in {GetType().Name}.");
        }

        protected ErrorDto PrepareError(ModelStateDictionary modelState)
        {
            ErrorDto validationError = new ErrorDto();

            foreach (string modelStateKey in modelState.Keys)
            {
                ModelStateEntry modelStateVal = ViewData.ModelState[modelStateKey];
                foreach (ModelError error in modelStateVal.Errors)
                {
                    string value = error.Exception != null ? error.Exception.Message : modelStateKey;
                    string desc = !string.IsNullOrWhiteSpace(error.ErrorMessage) ? error.ErrorMessage : "Exception occured";

                    validationError.Errors.Add(new StructDto<string, string>
                    {
                        Value = value,
                        Description = desc
                    });
                }
            }

            return validationError;
        }

        protected ErrorDto PrepareError(string value, string description)
        {
            ErrorDto e = new ErrorDto();

            e.Errors.Add(new StructDto<string, string>
            {
                Value = value,
                Description = description
            });

            return e;
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
