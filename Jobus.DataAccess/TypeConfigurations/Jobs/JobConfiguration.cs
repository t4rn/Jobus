using Jobus.Domain.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobus.DataAccess.TypeConfigurations.Jobs
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Input).HasColumnType("json");
            builder.Property(x => x.Output).HasColumnType("json");

            builder.Property(x => x.AddDate).HasDefaultValueSql("now()");
            //builder.Property(x => x.OutputDate);

            builder.Property(x => x.WsClientId).HasColumnName("id_ws_client");
            builder.HasOne(x => x.WsClient)
                .WithMany(ws => ws.Jobs);

            builder.Property(x => x.TypeCode).IsRequired().HasMaxLength(8).HasColumnName("job_type");
            builder.HasOne(x => x.Type)
                .WithMany(jt => jt.Jobs);

            builder.HasOne(x => x.JobQueue)
                .WithOne(qi => qi.Job)
                .HasForeignKey<JobQueue>(qi => qi.JobId);
        }
    }
}
