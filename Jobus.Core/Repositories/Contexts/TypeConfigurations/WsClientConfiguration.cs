using Jobus.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.Core.Repositories.Contexts.TypeConfigurations
{
    public class WsClientConfiguration : IEntityTypeConfiguration<WsClient>
    {
        public void Configure(EntityTypeBuilder<WsClient> builder)
        {
            builder.ToTable("wsClients");
            builder.HasIndex(wsClient => wsClient.Hash).IsUnique();
            builder.HasIndex(wsClient => wsClient.Name).IsUnique();

            builder.Property(wsClient => wsClient.Hash)
                    .HasColumnName("hash")
                    .HasMaxLength(16)
                    .IsRequired();

            builder.Property(wsClient => wsClient.Name)
                .HasColumnName("clientName")
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
