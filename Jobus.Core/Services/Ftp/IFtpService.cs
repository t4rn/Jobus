using Jobus.Common.Results;
using System.Collections.Generic;

namespace Jobus.Core.Services.Ftp
{
    public interface IFtpService
    {
        Result GetFilesFromFtp(string jobDirectory, IEnumerable<string> fileNames);
    }
}
