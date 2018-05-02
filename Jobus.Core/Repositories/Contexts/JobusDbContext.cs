using Jobus.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobus.Core.Repositories.Contexts
{
    public class JobusDbContext : DbContext
    {
        public JobusDbContext(DbContextOptions<JobusDbContext> options) : base(options)
        {

        }

        public DbSet<WsClient> WsClients { get; set; }
    }
}
