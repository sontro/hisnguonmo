using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionBillTwoBookSDO
    {
        public HIS_TRANSACTION RecieptTransaction { get; set; }
        public HIS_TRANSACTION InvoiceTransaction { get; set; }

        public List<HIS_SERE_SERV_BILL> RecieptSereServBills { get; set; }
        public List<HIS_SERE_SERV_BILL> InvoiceSereServBills { get; set; }

        public decimal RecieptPayAmount { get; set; }
        public decimal InvoicePayAmount { get; set; }
        public long RequestRoomId { get; set; }
        public long TreatmentId { get; set; }
        public bool IsAutoRepay { get; set; }
        public long? RepayAccountBookId { get; set; }
        public long? RepayNumOrder { get; set; }
    }
}
