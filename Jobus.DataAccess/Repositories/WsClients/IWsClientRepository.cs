using Jobus.Domain.WsClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobus.DataAccess.Repositories.WsClients
{
    public interface IWsClientRepository : IRepository
    {
        Task<IEnumerable<WsClient>> GetAllAsync(bool includeGhosts = false);
    }
}
