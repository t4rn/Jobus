namespace Jobus.Core.Services.Rest
{
    public class RestSettings
    {
        public string Url { get; set; }
        public Header[] Headers { get; set; }
    }
    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
