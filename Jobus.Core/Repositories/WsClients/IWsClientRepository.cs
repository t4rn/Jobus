using Jobus.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobus.Core.Repositories.WsClients
{
    public interface IWsClientRepository
    {
        Task<IEnumerable<WsClient>> GetAllAsync(bool includeGhosts = false);
    }
}
