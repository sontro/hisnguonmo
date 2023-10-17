using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class MerchantPaymentSDO
    {
        public string code { get; set; }
        public string message { get; set; }
        public string msgType { get; set; }
        public string txnId { get; set; }
        public string qrTrace { get; set; }
        public string bankCode { get; set; }
        public string mobile { get; set; }
        public string accountNo { get; set; }
        public string amount { get; set; }
        public string payDate { get; set; }
        public string merchantCode { get; set; }
        public string terminalId { get; set; }
        public string ccy { get; set; }
        public string checksum { get; set; }
        public List<MerchantPaymentDetailSDO> addData { get; set; }
    }

    public class MerchantPaymentDetailSDO
    {
        public string merchantType { get; set; }
        public string serviceCode { get; set; }
        public string masterMerCode { get; set; }
        public string merchantCode { get; set; }
        public string terminalId { get; set; }
        public string productId { get; set; }
        public string amount { get; set; }
        public string ccy { get; set; }
    }
}
