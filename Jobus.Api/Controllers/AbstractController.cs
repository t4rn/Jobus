using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Runtime.CompilerServices;

namespace Jobus.Api.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected readonly string _ip;

        public AbstractController(IActionContextAccessor actionContextAccessor)
        {
            _ip = actionContextAccessor?.ActionContext?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        protected string LogDescription([CallerMemberName] string methodName = null)
        {
            return $"[{methodName} - {_ip}]";
        }
    }
}
