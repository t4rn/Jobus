﻿using System;

namespace Jobus.Domain.WsClients
{
    public class WsClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public bool Ghost { get; set; }
        public DateTime AddDate { get; set; }
    }
}
