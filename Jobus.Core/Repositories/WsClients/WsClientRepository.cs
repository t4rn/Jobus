using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobus.Core.Repositories.Contexts;
using Jobus.Domain;
using Microsoft.EntityFrameworkCore;

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
            // TODO: includeGhosts
            return await _jobusDbContext.WsClients.ToListAsync();
        }
    }
}
