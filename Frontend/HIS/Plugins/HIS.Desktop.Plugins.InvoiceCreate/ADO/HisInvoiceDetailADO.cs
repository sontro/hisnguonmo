using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreate.ADO
{
    public class HisInvoiceDetailADO : HIS_INVOICE_DETAIL
    {
        public decimal TOTAL_PRICE { get; set; }
    }
}
