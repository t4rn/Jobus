using Jobus.Domain.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations.Dictionaries
{
    public class DictionaryConfiguration : IEntityTypeConfiguration<DictionaryItem>
    {
        public void Configure(EntityTypeBuilder<DictionaryItem> builder)
        {
            builder.HasKey(x => x.Code);

            builder.Property(x => x.Code).HasMaxLength(8);
            builder.Property(x => x.Description).HasMaxLength(32).IsRequired();

            builder.Property(x => x.Ghost).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.AddDate).HasDefaultValueSql("now()");
        }
    }
}
