using Jobus.Common.Results;
using Jobus.Domain.WsClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobus.Core.Services.WsClients
{
    public interface IWsClientService
    {
        Task<IEnumerable<WsClient>> GetWsClientsAsync(bool includeGhosts = false);
        Task<Result> AuthorizeAsync(string hash, string controllerName, string actionName);
    }
}
