using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public enum UpdateField
    {
        PARENT_ID,
        IS_EXPEND,
        STENT_ORDER,
        IS_OUT_PARENT_FEE,
        PATIENT_TYPE_ID,
        IS_NO_EXECUTE,
        IS_FUND_ACCEPTED,
        SHARE_COUNT,
        EQUIPMENT_SET_ORDER__AND__EQUIPMENT_SET_ID,
        PRIMARY_PATIENT_TYPE_ID,
        EXPEND_TYPE_ID,
        USER_PRICE,
        PACKAGE_PRICE,
        OTHER_PAY_SOURCE_ID,
        IS_NOT_USE_BHYT,
        SERVICE_CONDITION_ID
    }

    public class HisSereServPayslipSDO
    {
        public long TreatmentId { get; set; }
        public List<HIS_SERE_SERV> SereServs { get; set; }
        public UpdateField Field { get; set; }
    }
}
