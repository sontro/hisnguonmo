using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqExamRegisterSDO : AssignServiceSDO
    {
        //Profile data
        public HisPatientProfileSDO HisPatientProfile { get; set; }
        public long? AccountBookId { get; set; }
        public long? PayFormId { get; set; }
        public long? TransNumOrder { get; set; }
        public string CashierLoginName { get; set; }
        public string CashierUserName { get; set; }
        public long? CashierWorkingRoomId { get; set; }
        public bool IsAutoCreateBillForNonBhyt { get; set; }
        public bool IsAutoCreateDepositForNonBhyt { get; set; }
        public bool IsUsingEpayment { get; set; }
    }
}
