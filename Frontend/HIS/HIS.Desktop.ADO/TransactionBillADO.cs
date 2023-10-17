using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TransactionBillADO
    {
        public long TreatmentId { get; set; }
        public long CashierRoomId { get; set; }

        public TransactionBillADO(long treatmentId, long cashierRoomId)
        {
            this.CashierRoomId = cashierRoomId;
            this.TreatmentId = treatmentId;
        }
    }
}
