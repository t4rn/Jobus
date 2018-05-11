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
            builder.HasIndex(x => x.Hash).IsUnique().HasName("ws_clients_hash_unique");

            builder.Property(x => x.Name).HasColumnName("client_name").HasMaxLength(32).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique().HasName("ws_clients_name_unique");

            builder.Property(x => x.Ghost).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.AddDate).HasDefaultValueSql("now()");

            builder.ForNpgsqlHasComment("Table with api clients");
        }
    }
}
