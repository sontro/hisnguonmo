using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class PaymentVietinbankTDO
    {
        public string statusCode { get; set; }
        public string amount { get; set; }
        public string terminalId { get; set; }
        public string bankTransactionId { get; set; }
        public string requestId { get; set; }
        public string merchantName { get; set; }
        public string merchantId { get; set; }
        public string transactionDate { get; set; }
        public string orderId { get; set; }
        public string statusMessage { get; set; }
        public string productId { get; set; }
        public string signature { get; set; }
    }
}
