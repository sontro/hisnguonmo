using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class HisInvoiceADO : HIS_INVOICE
    {
        //public HisInvoiceADO();
        public List<long> SereServIds { get; set; }
        public string SYMBOL_CODE { get; set; }
        public string TEMPLATE_CODE { get; set; }
    }
}
