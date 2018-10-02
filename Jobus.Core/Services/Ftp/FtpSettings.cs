namespace Jobus.Core.Services.Ftp
{
    public class FtpSettings
    {
        public string FtpHost { get; set; }
        public int FtpPort { get; set; }
        public string FtpUser { get; set; }
        public string FtpPassword { get; set; }
        public int RetryAttempts { get; set; }
    }
}
