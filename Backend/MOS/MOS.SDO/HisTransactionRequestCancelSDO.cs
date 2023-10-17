using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionRequestCancelSDO
    {
        public long TransactionId { get; set; }
        public string CancelReqReason { get; set; }
        public long WorkingRoomId { get; set; }
    }
}
