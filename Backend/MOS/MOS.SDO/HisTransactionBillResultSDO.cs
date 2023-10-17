using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionBillResultSDO
    {
        public V_HIS_TRANSACTION TransactionBill { get; set; }
        public V_HIS_TRANSACTION TransactionRepay { get; set; }
        public List<HIS_SERE_SERV_BILL> SereServBills { get; set; }
    }
}
