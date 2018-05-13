using Jobus.Domain.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations.Dictionaries
{
    public class JobStatusConfiguration : BaseDictionaryConfiguration<JobStatus>
    {
        public override void Configure(EntityTypeBuilder<JobStatus> builder)
        {
            base.Configure(builder);
            builder.ToTable("dic_job_status");
        }
    }
}
