using Jobus.Domain.WsClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations.WsClients
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Controller).HasMaxLength(16).IsRequired();
            builder.Property(x => x.Action).HasMaxLength(32).IsRequired();
            builder.Property(x => x.AddDate).HasDefaultValueSql("now()");

            builder.HasIndex(x => new { x.Controller, x.Action }).IsUnique().HasName("controller_action_unique");

            builder.ForNpgsqlHasComment("Api resources");
        }
    }
}
