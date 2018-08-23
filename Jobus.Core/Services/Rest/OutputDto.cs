namespace Jobus.Core.Services.Rest
{
    public class OutputDto
    {
        public long timestamp { get; set; }
        public int status { get; set; }
        public string error { get; set; }
        public string exception { get; set; }
        public string message { get; set; }
        public string path { get; set; }
    }
}
