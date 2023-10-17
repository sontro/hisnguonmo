using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Base
{
    public class ElectronicBillResult
    {
        public bool Success { get; set; }
        public string InvoiceCode { get; set; }
        public string InvoiceSys { get; set; }
        public List<string> Messages { get; set; }
        public string InvoiceLink { get; set; }
        public string InvoiceNumOrder { get; set; }
        public string InvoiceLoginname { get; set; }
        public long? InvoiceTime { get; set; }
    }
}
