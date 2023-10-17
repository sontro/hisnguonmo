using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class BillTransferADO
    {
        public long TreatmentId { get; set; }
        public long CashierRoomId { get; set; }

        public BillTransferADO(long treatmentId, long cashierRoomId)
        {
            this.CashierRoomId = cashierRoomId;
            this.TreatmentId = treatmentId;
        }
    }
}
