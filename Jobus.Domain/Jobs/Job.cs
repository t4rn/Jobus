using Jobus.Domain.WsClients;
using System;

namespace Jobus.Domain.Jobs
{
    public class Job
    {
        public long Id { get; set; }

        public int WsClientId { get; set; }
        public WsClient WsClient { get; set; }

        public string TypeCode { get; set; }
        public JobType Type { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime? OutputDate { get; set; }

        public JobQueue QueueInfo { get; set; }
    }
}
