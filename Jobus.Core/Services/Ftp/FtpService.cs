using FluentFTP;
using Jobus.Common.Extensions;
using Jobus.Common.Results;
using Jobus.Core.Services.Loggers;
using System.Collections.Generic;
using System.Linq;

namespace Jobus.Core.Services.Ftp
{
    public class FtpService : IFtpService
    {
        private readonly ILogger _log;
        private readonly FtpSettings _ftpSettings;

        public FtpService(ILogger log, FtpSettings ftpSettings)
        {
            _log = log;
            _ftpSettings = ftpSettings;
        }
        public Result GetFilesFromFtp(string jobDirectory, IEnumerable<string> fileNames)
        {
            Result result = new Result();
            string methodName = nameof(GetFilesFromFtp);
            using (FtpClient client = new FtpClient(_ftpSettings.FtpHost, _ftpSettings.FtpPort, _ftpSettings.FtpUser, _ftpSettings.FtpPassword))
            {
                FtpListItem[] filesOnFtp = client.GetListing();

                _log.LogDebug("{MethodName} Ftp = {FtpAddress} contains = {FilesOnFtpCount} elements", methodName, client.Host, filesOnFtp?.Length);

                client.RetryAttempts = _ftpSettings.RetryAttempts;

                foreach (string fileName in fileNames.DefaultIfNull())
                {
                    if (!filesOnFtp.Any(x => x.Name == fileName))
                    {
                        _log.LogError("{MethodName} File {FileName} not found on FTP", methodName, fileName);
                        result.Message = $"File '{fileName}' not found on FTP.";
                        result.IsOk = false;
                        break;
                    }
                    else
                    {
                        // save file to temp dir
                        string localPath = $"{jobDirectory}\\{fileName}";
                        client.DownloadFile(localPath, $"{fileName}");

                        _log.LogDebug("{MethodName} File = {FileName} downloaded", methodName, localPath);

                        result.IsOk = true;
                    }
                }
            }

            return result;
        }
    }
}
