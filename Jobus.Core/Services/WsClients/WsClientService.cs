using Jobus.Core.Repositories.WsClients;
using Jobus.Core.Services.Cache;
using Jobus.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobus.Core.Services.WsClients
{
    public class WsClientService : IWsClientService
    {
        private readonly ICacheService _cacheService;
        private readonly IWsClientRepository _wsClientRepository;

        public WsClientService(ICacheService cacheService, IWsClientRepository wsClientRepository)
        {
            _cacheService = cacheService;
            _wsClientRepository = wsClientRepository;
        }

        public async Task<IEnumerable<WsClient>> GetWsClientsAsync(bool includeGhosts = false)
        {
            string cacheKey = "wsClients";
            
            // search in cache
            IEnumerable<WsClient> wsClients = _cacheService.Get<IEnumerable<WsClient>>(cacheKey);

            if (wsClients == null)
            {
                // get from db
                wsClients = await _wsClientRepository.GetAllAsync(includeGhosts);
                _cacheService.Set(cacheKey, wsClients);
            }

            return wsClients;
        }
    }
}
