using System;

namespace MOS.SDO
{
    public class HisTransactionCancelSDO
    {
        public long TransactionId { get; set; }
        public long RequestRoomId { get; set; }
        public string CancelReason { get; set; }
        public long? CancelReasonId { get; set; }
        public long CancelTime { get; set; }
        public bool IsInternal { get; set; }
    }
}
