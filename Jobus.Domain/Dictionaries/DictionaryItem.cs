using System;

namespace Jobus.Domain.Dictionaries
{
    public class DictionaryItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Ghost { get; set; }
        public DateTime AddDate { get; set; }
    }
}
