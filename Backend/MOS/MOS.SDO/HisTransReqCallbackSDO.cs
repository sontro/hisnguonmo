using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransReqCallbackSDO
    {
        public string TransReqCode { get; set; }
        public string TigTransactionCode { get; set; }
        public long? TigTransactionTime { get; set; }
        public bool IsSuccessful { get; set; }
        public decimal Amount { get; set; }
    }
}
