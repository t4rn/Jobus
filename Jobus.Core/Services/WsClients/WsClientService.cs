using Jobus.Core.Repositories.WsClient;
using Jobus.Core.Services.Cache;
using Jobus.Domain;
using System;
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

        public Task<IEnumerable<WsClient>> GetWsClientsAsync(bool includeGhosts = false)
        {
            // search in cache

            // not in cache -> get from db
            throw new NotImplementedException();
        }
    }
}
