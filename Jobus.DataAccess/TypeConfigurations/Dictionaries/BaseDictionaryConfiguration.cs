using Jobus.Domain.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations.Dictionaries
{
    public class BaseDictionaryConfiguration<T> : IEntityTypeConfiguration<T> where T : DictionaryItem
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Code);

            builder.Property(x => x.Code).HasMaxLength(8);
            builder.Property(x => x.Description).HasMaxLength(32).IsRequired();

            builder.Property(x => x.Ghost).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.AddDate).HasDefaultValueSql("now()");
        }
    }
}
