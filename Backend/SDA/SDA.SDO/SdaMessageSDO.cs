using SDA.EFMODEL.DataModels;
using System.Collections.Generic;

namespace SDA.SDO
{
    public class SdaMessageSDO
    {
        public SDA_MESSAGE Message { get; set; }
        public Dictionary<string, string> Receivers { get; set; }
    }
}
