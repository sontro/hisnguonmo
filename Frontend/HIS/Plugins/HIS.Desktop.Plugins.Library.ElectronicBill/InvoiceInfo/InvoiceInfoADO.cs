using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.InvoiceInfo
{
    class InvoiceInfoADO
    {
        public string BuyerOrganization { get; set; }
        public string BuyerAccountNumber { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string BuyerGender { get; set; }
        public string BuyerDob { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerTaxCode { get; set; }
        public string BuyerEmail { get; set; }
        public string Note { get; set; }
        public string PaymentMethod { get; set; }
        public long TransactionTime { get; set; }
    }
}
