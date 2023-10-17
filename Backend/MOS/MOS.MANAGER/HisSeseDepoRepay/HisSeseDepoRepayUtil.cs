using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    class HisSeseDepoRepayUtil
    {
        internal static void SetTdl(HIS_SESE_DEPO_REPAY seseDepoRepay, HIS_SERE_SERV_DEPOSIT sereServDeposit)
        {
            if (sereServDeposit != null && seseDepoRepay != null)
            {
                seseDepoRepay.TDL_AMOUNT = sereServDeposit.TDL_AMOUNT;
                seseDepoRepay.TDL_HEIN_LIMIT_PRICE = sereServDeposit.TDL_HEIN_LIMIT_PRICE;
                seseDepoRepay.TDL_IS_EXPEND = sereServDeposit.TDL_IS_EXPEND;
                seseDepoRepay.TDL_PATIENT_TYPE_ID = sereServDeposit.TDL_PATIENT_TYPE_ID;
                seseDepoRepay.TDL_SERVICE_CODE = sereServDeposit.TDL_SERVICE_CODE;
                seseDepoRepay.TDL_SERVICE_ID = sereServDeposit.TDL_SERVICE_ID;
                seseDepoRepay.TDL_SERVICE_NAME = sereServDeposit.TDL_SERVICE_NAME;
                seseDepoRepay.TDL_SERVICE_REQ_ID = sereServDeposit.TDL_SERVICE_REQ_ID.Value;
                seseDepoRepay.TDL_SERVICE_TYPE_ID = sereServDeposit.TDL_SERVICE_TYPE_ID;
                seseDepoRepay.TDL_SERVICE_UNIT_ID = sereServDeposit.TDL_SERVICE_UNIT_ID;
                seseDepoRepay.TDL_TREATMENT_ID = sereServDeposit.TDL_TREATMENT_ID;
                seseDepoRepay.TDL_VIR_PRICE = sereServDeposit.TDL_VIR_PRICE;
                seseDepoRepay.TDL_VIR_TOTAL_HEIN_PRICE = sereServDeposit.TDL_VIR_TOTAL_HEIN_PRICE;
                seseDepoRepay.TDL_VIR_TOTAL_PATIENT_PRICE = sereServDeposit.TDL_VIR_TOTAL_PATIENT_PRICE;
                seseDepoRepay.TDL_VIR_TOTAL_PRICE = sereServDeposit.TDL_VIR_TOTAL_PRICE;
                seseDepoRepay.TDL_EXECUTE_DEPARTMENT_ID = sereServDeposit.TDL_EXECUTE_DEPARTMENT_ID;
                seseDepoRepay.TDL_HEIN_SERVICE_TYPE_ID = sereServDeposit.TDL_HEIN_SERVICE_TYPE_ID;
                seseDepoRepay.TDL_IS_OUT_PARENT_FEE = sereServDeposit.TDL_IS_OUT_PARENT_FEE;
                seseDepoRepay.TDL_REQUEST_DEPARTMENT_ID = sereServDeposit.TDL_REQUEST_DEPARTMENT_ID;
                seseDepoRepay.TDL_SERE_SERV_PARENT_ID = sereServDeposit.TDL_SERE_SERV_PARENT_ID;
                seseDepoRepay.TDL_VIR_HEIN_PRICE = sereServDeposit.TDL_VIR_HEIN_PRICE;
                seseDepoRepay.TDL_VIR_PRICE_NO_ADD_PRICE = sereServDeposit.TDL_VIR_PRICE_NO_ADD_PRICE;
            }
        }
    }
}
