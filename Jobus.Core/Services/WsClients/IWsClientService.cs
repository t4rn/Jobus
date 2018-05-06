using Jobus.Common.Results;
using Jobus.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobus.Core.Services.WsClients
{
    public interface IWsClientService
    {
        Task<IEnumerable<WsClient>> GetWsClientsAsync(bool includeGhosts = false);
        Task<bool> IsHashCorrectAsync(string hash);
    }
}
