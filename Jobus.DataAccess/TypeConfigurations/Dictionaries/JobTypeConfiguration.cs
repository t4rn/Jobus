using Jobus.Domain.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations.Dictionaries
{
    public class JobTypeConfiguration : BaseDictionaryConfiguration<JobType>
    {
        public override void Configure(EntityTypeBuilder<JobType> builder)
        {
            base.Configure(builder);
            builder.ToTable("dic_job_type");
        }
    }
}
