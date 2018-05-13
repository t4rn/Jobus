using Jobus.Domain.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobus.DataAccess.TypeConfigurations.Jobs
{
    public class JobQueueConfiguration : IEntityTypeConfiguration<JobQueue>
    {
        public void Configure(EntityTypeBuilder<JobQueue> builder)
        {
            builder.HasKey(x => x.JobId);
        }
    }
}
//public long JobId { get; set; }
//public Job Job { get; set; }

//public string TypeCode { get; set; }
//public JobType Type { get; set; }

//public string StatusCode { get; set; }
//public JobStatus Status { get; set; }

//public DateTime AddDate { get; set; }

//public DateTime? ProcessStartDate { get; set; }

//public DateTime? ProcessEndDate { get; set; }

//public string ErrorMsg { get; set; }