using Jobus.Common.Results;
using Jobus.Core.Services.Cache;
using Jobus.DataAccess.Repositories;
using Jobus.Domain.WsClients;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Result> AuthorizeAsync(string hash, string controllerName, string actionName)
        {
            Result result = new Result();
            IEnumerable<WsClient> wsClients = await GetWsClientsAsync();

            WsClient foundWsClient = wsClients.FirstOrDefault(x => x.Hash == hash);

            if (foundWsClient == null)
            {
                result.Message = $"Incorrect hash '{hash}'.";
            }
            // TODO: check, if it's ADMIN -> allow all
            else if (!foundWsClient.ClientsResources.Any(cr =>
                cr.Resource.Controller.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase) &&
                cr.Resource.Action.Equals(actionName, StringComparison.InvariantCultureIgnoreCase)))
            {
                result.Message = $"Client '{foundWsClient.Name}' unauthorized for Action '{actionName}' in Controller '{controllerName}'.";
            }
            else
            {
                result.IsOk = true;
            }

            return result;
        }
    }
}
