using Jobus.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.Core.Repositories.Contexts.TypeConfigurations
{
    public class WsClientConfiguration : IEntityTypeConfiguration<WsClient>
    {
        public void Configure(EntityTypeBuilder<WsClient> builder)
        {
            //builder.ToTable("ws_clients"); <- set in JobusDbConext by SnakeCase

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Hash).HasMaxLength(16).IsRequired();
            builder.HasIndex(x => x.Hash).IsUnique();

            builder.Property(x => x.Name).HasMaxLength(32).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Ghost).IsRequired();
            builder.Property(x => x.AddDate).HasDefaultValueSql("now()");

            builder.ForNpgsqlHasComment("Table with api clients");
        }
    }
}
