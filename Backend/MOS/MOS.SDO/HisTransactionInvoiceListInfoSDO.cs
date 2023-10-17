using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionInvoiceListInfoSDO
    {
        public List<long> Ids { get; set; }
        public string InvoiceSys { get; set; }
        public string InvoiceCode { get; set; }
        public string EinvoiceNumOrder { get; set; }
        public long EInvoiceTime { get; set; }
        public string EinvoiceLoginname { get; set; }
    }
}
