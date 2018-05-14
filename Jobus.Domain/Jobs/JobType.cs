using Jobus.Domain.Dictionaries;
using System.Collections.Generic;

namespace Jobus.Domain.Jobs
{
    public class JobType : DictionaryItem
    {
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<JobQueue> JobQueues { get; set; }

        public enum JobTypeCode
        {
        }
    }
}
