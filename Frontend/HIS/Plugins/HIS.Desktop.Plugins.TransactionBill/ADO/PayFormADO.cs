using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBill.ADO
{
    public class PayFormADO : MOS.EFMODEL.DataModels.HIS_PAY_FORM
    {
        public string PayFormId { get; set; }
        public long? BANK_ID { get; set; }
    }
}
