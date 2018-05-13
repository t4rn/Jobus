namespace Jobus.Domain.WsClients
{
    public class WsClientResource
    {
        public int WsClientId { get; set; }
        public WsClient WsClient { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
