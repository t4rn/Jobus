using Jobus.Domain.WsClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations
{
    public class WsClientResourceConfiguration : IEntityTypeConfiguration<WsClientResource>
    {
        public void Configure(EntityTypeBuilder<WsClientResource> builder)
        {
            builder.HasKey(x => new { x.WsClientId, x.ResourceId });

            builder.Property(x => x.WsClientId).HasColumnName("id_ws_client");
            builder.Property(x => x.ResourceId).HasColumnName("id_resource");

            builder.HasOne(x => x.WsClient)
                .WithMany(wsClient => wsClient.ClientsResources)
                .HasForeignKey(wp => wp.WsClientId);

            builder.HasOne(x => x.Resource)
                .WithMany(p => p.ClientsPermissions)
                .HasForeignKey(p => p.ResourceId);

            builder.ForNpgsqlHasComment("Table with controllers and actions allowed for api clients");
        }
    }
}
