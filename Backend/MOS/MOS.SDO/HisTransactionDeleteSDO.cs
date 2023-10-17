using System;

namespace MOS.SDO
{
    public class HisTransactionDeleteSDO
    {
        public long TransactionId { get; set; }
        public long RequestRoomId { get; set; }
        public string DeleteReason { get; set; }
    }
}
