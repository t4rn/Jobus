using Jobus.Common.Results;

namespace Jobus.Core.Services.Rest
{
    public interface IRestService
    {
        Result SendRequest(int jobId, string body);
    }
}
