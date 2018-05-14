using Jobus.DataAccess.Contexts;
using Jobus.Domain.WsClients;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobus.DataAccess.Repositories.WsClients
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
            IQueryable<WsClient> query = _jobusDbContext.WsClients.AsNoTracking()
                .Include(ws => ws.ClientsResources)
                .ThenInclude(cr => cr.Resource);

            if (!includeGhosts)
            {
                query = query.Where(x => x.Ghost == false);
            }

            IEnumerable<WsClient> wsClients = await query.ToListAsync();

            return wsClients;
        }
    }
}
