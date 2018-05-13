using Jobus.Domain.WsClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobus.DataAccess.Repositories
{
    public interface IWsClientRepository
    {
        Task<IEnumerable<WsClient>> GetAllAsync(bool includeGhosts = false);
    }
}
