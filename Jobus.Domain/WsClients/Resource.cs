using System;
using System.Collections.Generic;

namespace Jobus.Domain.WsClients
{
    public class Resource
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime AddDate { get; set; }

        public IEnumerable<WsClientResource> ClientsPermissions { get; set; }
    }
}
