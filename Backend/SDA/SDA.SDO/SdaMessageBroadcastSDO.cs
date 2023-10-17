using System.Collections.Generic;

namespace SDA.SDO
{
    public class SdaMessageBroadcastSDO
    {
        public long MessageId { get; set; }
        public string Creator { get; set; }
        public Dictionary<string, string> Receivers { get; set; }
    }
}
