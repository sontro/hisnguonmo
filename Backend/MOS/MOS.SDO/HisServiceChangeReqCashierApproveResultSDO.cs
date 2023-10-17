using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceChangeReqCashierApproveResultSDO
    {
        public HIS_SERVICE_CHANGE_REQ ServiceChangeReq { get; set; }
        public HIS_TRANSACTION Repay { get; set; }
        public HIS_TRANSACTION Deposit { get; set; }
        public HIS_SESE_DEPO_REPAY SeseDepoRepay { get; set; }
        public HIS_SERE_SERV_DEPOSIT SereServDeposit { get; set; }
    }
}
