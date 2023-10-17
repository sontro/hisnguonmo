using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisInvoiceUpdateInfoSDO
    {
        public long InvoiceId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerAccountNumber { get; set; }
        public string BuyerTaxCode { get; set; }
        public string BuyerOrganization { get; set; }
    }
}
