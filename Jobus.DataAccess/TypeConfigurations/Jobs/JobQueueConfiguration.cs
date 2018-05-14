using Jobus.Domain.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations.Jobs
{
    public class JobQueueConfiguration : IEntityTypeConfiguration<JobQueue>
    {
        public void Configure(EntityTypeBuilder<JobQueue> builder)
        {
            builder.HasKey(x => x.JobId);

            builder.Property(x => x.JobId).HasColumnName("id_job");
            builder.Property(x => x.TypeCode).IsRequired().HasMaxLength(8).HasColumnName("job_type");
            builder.HasOne(x => x.Type)
                .WithMany(jt => jt.JobQueues);

            builder.Property(x => x.StatusCode).IsRequired().HasMaxLength(8).HasColumnName("job_status");
            builder.HasOne(x => x.Status)
                .WithMany(st => st.JobQueues);

            builder.Property(x => x.AddDate).HasDefaultValueSql("now()");
        }
    }
}
