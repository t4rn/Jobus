namespace Jobus.Core.Services.Loggers
{
    public class SerilogSettings
    {
        public string Uri { get; set; }
        public string LogLevel { get; set; }
        public string IndexFormat { get; set; }
        public string FileDebugPath { get; set; }
        public string FileErrorPath { get; set; }
        public string FileAppPath { get; set; }
        public string OutputTemplate { get; set; }
    }
}
