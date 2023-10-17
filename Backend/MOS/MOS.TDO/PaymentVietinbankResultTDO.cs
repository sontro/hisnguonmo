using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class PaymentVietinbankResultTDO
    {
        public string requestId { get; set; }
        public string paymentStatus { get; set; }
        public string signature { get; set; }
    }
}
