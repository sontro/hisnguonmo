using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServDeposit
{
    class HisSereServDepositUtil
    {
        internal static void SetTdl(HIS_SERE_SERV_DEPOSIT sereServDeposit, HIS_SERE_SERV sereServ)
        {
            if (sereServDeposit != null && sereServ != null)
            {
                sereServDeposit.TDL_AMOUNT = sereServ.AMOUNT;
                sereServDeposit.TDL_HEIN_LIMIT_PRICE = sereServ.HEIN_LIMIT_PRICE;
                sereServDeposit.TDL_IS_EXPEND = sereServ.IS_EXPEND;
                sereServDeposit.TDL_PATIENT_TYPE_ID = sereServ.PATIENT_TYPE_ID;
                sereServDeposit.TDL_SERVICE_CODE = sereServ.TDL_SERVICE_CODE;
                sereServDeposit.TDL_SERVICE_ID = sereServ.SERVICE_ID;
                sereServDeposit.TDL_SERVICE_NAME = sereServ.TDL_SERVICE_NAME;
                sereServDeposit.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID.HasValue ? sereServ.SERVICE_REQ_ID.Value : 0;
                sereServDeposit.TDL_SERVICE_TYPE_ID = sereServ.TDL_SERVICE_TYPE_ID;
                sereServDeposit.TDL_SERVICE_UNIT_ID = sereServ.TDL_SERVICE_UNIT_ID;
                sereServDeposit.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID.HasValue ? sereServ.TDL_TREATMENT_ID.Value : 0;
                sereServDeposit.TDL_VIR_PRICE = sereServ.VIR_PRICE.HasValue ? sereServ.VIR_PRICE.Value : 0;
                sereServDeposit.TDL_VIR_TOTAL_HEIN_PRICE = sereServ.VIR_TOTAL_HEIN_PRICE.HasValue ? sereServ.VIR_TOTAL_HEIN_PRICE.Value : 0;
                sereServDeposit.TDL_VIR_TOTAL_PATIENT_PRICE = sereServ.VIR_TOTAL_PATIENT_PRICE.HasValue ? sereServ.VIR_TOTAL_PATIENT_PRICE.Value : 0;
                sereServDeposit.TDL_VIR_TOTAL_PRICE = sereServ.VIR_TOTAL_PRICE.HasValue ? sereServ.VIR_TOTAL_PRICE.Value : 0;
                sereServDeposit.TDL_EXECUTE_DEPARTMENT_ID = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                sereServDeposit.TDL_HEIN_SERVICE_TYPE_ID = sereServ.TDL_HEIN_SERVICE_TYPE_ID;
                sereServDeposit.TDL_IS_OUT_PARENT_FEE = sereServ.IS_OUT_PARENT_FEE;
                sereServDeposit.TDL_REQUEST_DEPARTMENT_ID = sereServ.TDL_REQUEST_DEPARTMENT_ID;
                sereServDeposit.TDL_SERE_SERV_PARENT_ID = sereServ.PARENT_ID;
                sereServDeposit.TDL_VIR_HEIN_PRICE = sereServ.VIR_HEIN_PRICE ?? 0;
                sereServDeposit.TDL_VIR_PRICE_NO_ADD_PRICE = sereServ.VIR_PRICE_NO_ADD_PRICE ?? 0;
            }
        }

        internal static void SetTdl(HIS_SERE_SERV_DEPOSIT sereServDeposit, V_HIS_SERE_SERV sereServ)
        {
            if (sereServDeposit != null && sereServ != null)
            {
                sereServDeposit.TDL_AMOUNT = sereServ.AMOUNT;
                sereServDeposit.TDL_HEIN_LIMIT_PRICE = sereServ.HEIN_LIMIT_PRICE;
                sereServDeposit.TDL_IS_EXPEND = sereServ.IS_EXPEND;
                sereServDeposit.TDL_PATIENT_TYPE_ID = sereServ.PATIENT_TYPE_ID;
                sereServDeposit.TDL_SERVICE_CODE = sereServ.TDL_SERVICE_CODE;
                sereServDeposit.TDL_SERVICE_ID = sereServ.SERVICE_ID;
                sereServDeposit.TDL_SERVICE_NAME = sereServ.TDL_SERVICE_NAME;
                sereServDeposit.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID.HasValue ? sereServ.SERVICE_REQ_ID.Value : 0;
                sereServDeposit.TDL_SERVICE_TYPE_ID = sereServ.TDL_SERVICE_TYPE_ID;
                sereServDeposit.TDL_SERVICE_UNIT_ID = sereServ.TDL_SERVICE_UNIT_ID;
                sereServDeposit.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID.HasValue ? sereServ.TDL_TREATMENT_ID.Value : 0;
                sereServDeposit.TDL_VIR_PRICE = sereServ.VIR_PRICE.HasValue ? sereServ.VIR_PRICE.Value : 0;
                sereServDeposit.TDL_VIR_TOTAL_HEIN_PRICE = sereServ.VIR_TOTAL_HEIN_PRICE.HasValue ? sereServ.VIR_TOTAL_HEIN_PRICE.Value : 0;
                sereServDeposit.TDL_VIR_TOTAL_PATIENT_PRICE = sereServ.VIR_TOTAL_PATIENT_PRICE.HasValue ? sereServ.VIR_TOTAL_PATIENT_PRICE.Value : 0;
                sereServDeposit.TDL_VIR_TOTAL_PRICE = sereServ.VIR_TOTAL_PRICE.HasValue ? sereServ.VIR_TOTAL_PRICE.Value : 0;
                sereServDeposit.TDL_EXECUTE_DEPARTMENT_ID = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                sereServDeposit.TDL_HEIN_SERVICE_TYPE_ID = sereServ.TDL_HEIN_SERVICE_TYPE_ID;
                sereServDeposit.TDL_IS_OUT_PARENT_FEE = sereServ.IS_OUT_PARENT_FEE;
                sereServDeposit.TDL_REQUEST_DEPARTMENT_ID = sereServ.TDL_REQUEST_DEPARTMENT_ID;
                sereServDeposit.TDL_SERE_SERV_PARENT_ID = sereServ.PARENT_ID;
                sereServDeposit.TDL_VIR_HEIN_PRICE = sereServ.VIR_HEIN_PRICE ?? 0;
                sereServDeposit.TDL_VIR_PRICE_NO_ADD_PRICE = sereServ.VIR_PRICE_NO_ADD_PRICE ?? 0;
            }
        }
    }
}
