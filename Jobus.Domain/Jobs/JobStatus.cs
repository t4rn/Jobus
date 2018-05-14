using System.Collections.Generic;
using Jobus.Domain.Dictionaries;

namespace Jobus.Domain.Jobs
{
    public class JobStatus :DictionaryItem
    {
        public IEnumerable<JobQueue> JobQueues { get; set; }

        public enum JobStatusCode
        {
            WAIT, PROC, DONE, HOLD, ERR
        }
    }
}
