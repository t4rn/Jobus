using Jobus.Domain;
using Microsoft.EntityFrameworkCore;

namespace Jobus.Core.Repositories.Contexts
{
    public class JobusDbContext : DbContext
    {
        public JobusDbContext(DbContextOptions<JobusDbContext> options) : base(options)
        {

        }

        public DbSet<WsClient> WsClients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WsClientConfiguration());
        }
    }
}
