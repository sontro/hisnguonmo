using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceChangeReqCashierApproveSDO
    {
        public long ServiceChangeReqId { get; set; }
        public long WorkingRoomId { get; set; }
        public long TransactionTime { get; set; }

        public long? RepayAccountBookId { get; set; }
        public long? RepayNumOrder { get; set; }
        public decimal? RepayAmount { get; set; }
        public long? SereServDepositId { get; set; }
        public long? RepayPayFormId {get;set;}
        public long? RepayReasonId {get;set;}
        public decimal? RepayTransferAmount {get;set;}

        public long DepositAccountBookId { get; set; }
        public long? DepositNumOrder { get; set; }
        public decimal DepositAmount { get; set; }
        public long DepositPayFormId { get; set; }
        public long NewSereServId { get; set; }
        public decimal? DepositTransferAmount { get; set; }
    }
}
