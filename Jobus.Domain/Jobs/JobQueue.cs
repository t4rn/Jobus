using System;

namespace Jobus.Domain.Jobs
{
    public class JobQueue
    {
        public long JobId { get; set; }
        public Job Job { get; set; }

        public string TypeCode { get; set; }
        public JobType Type { get; set; }

        public string StatusCode { get; set; }
        public JobStatus Status { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime? ProcessStartDate { get; set; }

        public DateTime? ProcessEndDate { get; set; }

        public string ErrorMsg { get; set; }
    }
}
