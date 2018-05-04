using Jobus.Core.Repositories.Contexts.TypeConfigurations;
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
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new WsClientConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
