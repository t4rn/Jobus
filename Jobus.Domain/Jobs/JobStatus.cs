using Jobus.Domain.Dictionaries;

namespace Jobus.Domain.Jobs
{
    public class JobStatus :DictionaryItem
    {
        public enum JobStatusCode
        {
            WAIT, PROC, DONE, HOLD, ERR
        }
    }
}
