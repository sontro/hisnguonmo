using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisInvoiceSDO : HIS_INVOICE
    {
        public List<long> SereServIds { get; set; }
    }
}
