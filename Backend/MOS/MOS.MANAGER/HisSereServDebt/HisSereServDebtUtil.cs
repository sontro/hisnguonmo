using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServDebt
{
    class HisSereServDebtUtil
    {
        internal static void SetTdl(HIS_SERE_SERV_DEBT ssDebt, HIS_SERE_SERV sereServ)
        {
            if (ssDebt != null && sereServ != null)
            {
                ssDebt.TDL_AMOUNT = sereServ.AMOUNT;
                ssDebt.TDL_EXECUTE_DEPARTMENT_ID = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                ssDebt.TDL_HEIN_LIMIT_PRICE = sereServ.HEIN_LIMIT_PRICE;
                ssDebt.TDL_HEIN_SERVICE_TYPE_ID = sereServ.TDL_HEIN_SERVICE_TYPE_ID;
                ssDebt.TDL_IS_OUT_PARENT_FEE = sereServ.IS_OUT_PARENT_FEE;
                ssDebt.TDL_IS_EXPEND = sereServ.IS_EXPEND;
                ssDebt.TDL_PATIENT_TYPE_ID = sereServ.PATIENT_TYPE_ID;
                ssDebt.TDL_REQUEST_DEPARTMENT_ID = sereServ.TDL_REQUEST_DEPARTMENT_ID;
                ssDebt.TDL_SERE_SERV_PARENT_ID = sereServ.PARENT_ID;
                ssDebt.TDL_SERVICE_CODE = sereServ.TDL_SERVICE_CODE;
                ssDebt.TDL_SERVICE_ID = sereServ.SERVICE_ID;
                ssDebt.TDL_SERVICE_NAME = sereServ.TDL_SERVICE_NAME;
                ssDebt.TDL_SERVICE_TYPE_ID = sereServ.TDL_SERVICE_TYPE_ID;
                ssDebt.TDL_SERVICE_UNIT_ID = sereServ.TDL_SERVICE_UNIT_ID;
                ssDebt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
            }
        }

        internal static void SetPreviousDebtAmount(HIS_SERE_SERV_DEBT ssDebt, List<HIS_SERE_SERV_DEBT> oldSsDebts)
        {
            if (ssDebt != null)
            {
                decimal preAmount = oldSsDebts != null ? oldSsDebts.Sum(s => s.DEBT_PRICE) : (decimal)0;
                ssDebt.TOTAL_PREVIOUS_DEBT_PRICE = preAmount;
            }
        }
    }
}
