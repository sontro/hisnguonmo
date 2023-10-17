using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionBillByDepositSDO
    {
        public long TreatmentId { get; set; }
        public long AccountBookId { get; set; }
        public long PayformId { get; set; }
        public long TransactionTime { get; set; }
        public long? NumOrder { get; set; }
        public long WorkingRoomId { get; set; }
        public bool IsSplitByCashierDeposit { get; set; }
    }
}
