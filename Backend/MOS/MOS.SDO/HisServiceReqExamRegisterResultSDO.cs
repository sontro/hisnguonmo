using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqExamRegisterResultSDO : HisServiceReqListResultSDO
    {
        //Thong tin benh nhan
        public HisPatientProfileSDO HisPatientProfile { get; set; }
        //Cac giao dich da duoc thu tien (epayment)
        public List<V_HIS_TRANSACTION> CollectedTransactions { get; set; }
    }
}
