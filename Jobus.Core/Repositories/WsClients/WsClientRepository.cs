using Jobus.Core.Repositories.Contexts;
using Jobus.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobus.Core.Repositories.WsClients
{
    public class WsClientRepository : IWsClientRepository
    {
        private readonly JobusDbContext _jobusDbContext;

        public WsClientRepository(JobusDbContext jobusDbContext)
        {
            _jobusDbContext = jobusDbContext;
        }

        public async Task<IEnumerable<WsClient>> GetAllAsync(bool includeGhosts = false)
        {
            IQueryable<WsClient> query = _jobusDbContext.WsClients.AsNoTracking();

            if (!includeGhosts)
            {
                query = query.Where(x => x.Ghost == false);
            }

            IEnumerable<WsClient> wsClients = await query.ToListAsync();

            return wsClients;
        }
    }
}
