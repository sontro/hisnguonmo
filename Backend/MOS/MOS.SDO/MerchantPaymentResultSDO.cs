using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class MerchantPaymentResultSDO
    {
        public string code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string checksum { get; set; }
    }
}
