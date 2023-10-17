using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServBill
{
    class HisSereServBillUtil
    {
        internal static void SetTdl(HIS_SERE_SERV_BILL ssBill, HIS_SERE_SERV sereServ)
        {
            if (ssBill != null && sereServ != null)
            {
                ssBill.TDL_ADD_PRICE = sereServ.ADD_PRICE;
                ssBill.TDL_AMOUNT = sereServ.AMOUNT;
                ssBill.TDL_DISCOUNT = sereServ.DISCOUNT;
                ssBill.TDL_EXECUTE_DEPARTMENT_ID = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                ssBill.TDL_HEIN_LIMIT_PRICE = sereServ.HEIN_LIMIT_PRICE;
                ssBill.TDL_HEIN_LIMIT_RATIO = sereServ.HEIN_LIMIT_RATIO;
                ssBill.TDL_HEIN_NORMAL_PRICE = sereServ.HEIN_NORMAL_PRICE;
                ssBill.TDL_HEIN_PRICE = sereServ.HEIN_PRICE;
                ssBill.TDL_HEIN_RATIO = sereServ.HEIN_RATIO;
                ssBill.TDL_HEIN_SERVICE_TYPE_ID = sereServ.TDL_HEIN_SERVICE_TYPE_ID;
                ssBill.TDL_IS_OUT_PARENT_FEE = sereServ.IS_OUT_PARENT_FEE;
                ssBill.TDL_LIMIT_PRICE = sereServ.LIMIT_PRICE;
                ssBill.TDL_ORIGINAL_PRICE = sereServ.ORIGINAL_PRICE;
                ssBill.TDL_OTHER_SOURCE_PRICE = sereServ.OTHER_SOURCE_PRICE;
                ssBill.TDL_OVERTIME_PRICE = sereServ.OVERTIME_PRICE;
                ssBill.TDL_PATIENT_TYPE_ID = sereServ.PATIENT_TYPE_ID;
                ssBill.TDL_PRICE = sereServ.PRICE;
                ssBill.TDL_PRIMARY_PRICE = sereServ.PRIMARY_PRICE;
                ssBill.TDL_REQUEST_DEPARTMENT_ID = sereServ.TDL_REQUEST_DEPARTMENT_ID;
                ssBill.TDL_SERE_SERV_PARENT_ID = sereServ.PARENT_ID;
                ssBill.TDL_SERVICE_CODE = sereServ.TDL_SERVICE_CODE;
                ssBill.TDL_SERVICE_ID = sereServ.SERVICE_ID;
                ssBill.TDL_SERVICE_NAME = sereServ.TDL_SERVICE_NAME;
                ssBill.TDL_SERVICE_TYPE_ID = sereServ.TDL_SERVICE_TYPE_ID;
                ssBill.TDL_SERVICE_UNIT_ID = sereServ.TDL_SERVICE_UNIT_ID;
                ssBill.TDL_TOTAL_HEIN_PRICE = sereServ.VIR_TOTAL_HEIN_PRICE;
                ssBill.TDL_TOTAL_PATIENT_PRICE = sereServ.VIR_TOTAL_PATIENT_PRICE;
                ssBill.TDL_TOTAL_PATIENT_PRICE_BHYT = sereServ.VIR_TOTAL_PATIENT_PRICE_BHYT;
                ssBill.TDL_USER_PRICE = sereServ.USER_PRICE;
                ssBill.TDL_VAT_RATIO = sereServ.VAT_RATIO;
                ssBill.TDL_REAL_HEIN_PRICE = sereServ.VIR_HEIN_PRICE;
                ssBill.TDL_REAL_PATIENT_PRICE = sereServ.VIR_PATIENT_PRICE;
                ssBill.TDL_REAL_PRICE = sereServ.VIR_PRICE;
                ssBill.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                ssBill.TDL_PRIMARY_PATIENT_TYPE_ID = sereServ.PRIMARY_PATIENT_TYPE_ID;
            }
        }

        internal static void SetTdl(HIS_SERE_SERV_BILL ssBill, V_HIS_SERE_SERV sereServ)
        {
            if (ssBill != null && sereServ != null)
            {
                ssBill.TDL_ADD_PRICE = sereServ.ADD_PRICE;
                ssBill.TDL_AMOUNT = sereServ.AMOUNT;
                ssBill.TDL_DISCOUNT = sereServ.DISCOUNT;
                ssBill.TDL_EXECUTE_DEPARTMENT_ID = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                ssBill.TDL_HEIN_LIMIT_PRICE = sereServ.HEIN_LIMIT_PRICE;
                ssBill.TDL_HEIN_LIMIT_RATIO = sereServ.HEIN_LIMIT_RATIO;
                ssBill.TDL_HEIN_NORMAL_PRICE = sereServ.HEIN_NORMAL_PRICE;
                ssBill.TDL_HEIN_PRICE = sereServ.HEIN_PRICE;
                ssBill.TDL_HEIN_RATIO = sereServ.HEIN_RATIO;
                ssBill.TDL_HEIN_SERVICE_TYPE_ID = sereServ.TDL_HEIN_SERVICE_TYPE_ID;
                ssBill.TDL_IS_OUT_PARENT_FEE = sereServ.IS_OUT_PARENT_FEE;
                ssBill.TDL_LIMIT_PRICE = sereServ.LIMIT_PRICE;
                ssBill.TDL_ORIGINAL_PRICE = sereServ.ORIGINAL_PRICE;
                ssBill.TDL_OTHER_SOURCE_PRICE = sereServ.OTHER_SOURCE_PRICE;
                ssBill.TDL_OVERTIME_PRICE = sereServ.OVERTIME_PRICE;
                ssBill.TDL_PATIENT_TYPE_ID = sereServ.PATIENT_TYPE_ID;
                ssBill.TDL_PRICE = sereServ.PRICE;
                ssBill.TDL_PRIMARY_PRICE = sereServ.PRIMARY_PRICE;
                ssBill.TDL_REQUEST_DEPARTMENT_ID = sereServ.TDL_REQUEST_DEPARTMENT_ID;
                ssBill.TDL_SERE_SERV_PARENT_ID = sereServ.PARENT_ID;
                ssBill.TDL_SERVICE_CODE = sereServ.TDL_SERVICE_CODE;
                ssBill.TDL_SERVICE_ID = sereServ.SERVICE_ID;
                ssBill.TDL_SERVICE_NAME = sereServ.TDL_SERVICE_NAME;
                ssBill.TDL_SERVICE_TYPE_ID = sereServ.TDL_SERVICE_TYPE_ID;
                ssBill.TDL_SERVICE_UNIT_ID = sereServ.TDL_SERVICE_UNIT_ID;
                ssBill.TDL_TOTAL_HEIN_PRICE = sereServ.VIR_TOTAL_HEIN_PRICE;
                ssBill.TDL_TOTAL_PATIENT_PRICE = sereServ.VIR_TOTAL_PATIENT_PRICE;
                ssBill.TDL_TOTAL_PATIENT_PRICE_BHYT = sereServ.VIR_TOTAL_PATIENT_PRICE_BHYT;
                ssBill.TDL_USER_PRICE = sereServ.USER_PRICE;
                ssBill.TDL_VAT_RATIO = sereServ.VAT_RATIO;
                ssBill.TDL_REAL_HEIN_PRICE = sereServ.VIR_HEIN_PRICE;
                ssBill.TDL_REAL_PATIENT_PRICE = sereServ.VIR_PATIENT_PRICE;
                ssBill.TDL_REAL_PRICE = sereServ.VIR_PRICE;
                ssBill.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                ssBill.TDL_PRIMARY_PATIENT_TYPE_ID = sereServ.PRIMARY_PATIENT_TYPE_ID;
            }
        }
    }
}
