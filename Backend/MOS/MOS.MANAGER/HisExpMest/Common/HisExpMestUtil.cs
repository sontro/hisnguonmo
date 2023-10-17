using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common
{
    class HisExpMestUtil
    {
        public static void SetTdl(HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq, bool isSale)
        {
            if (expMest != null && serviceReq != null)
            {
                if (isSale)
                {
                    expMest.PRESCRIPTION_ID = serviceReq.ID;
                    expMest.TDL_PRESCRIPTION_REQ_LOGINNAME = serviceReq.REQUEST_LOGINNAME;
                    expMest.TDL_PRESCRIPTION_REQ_USERNAME = serviceReq.REQUEST_USERNAME;
                    expMest.TDL_PRES_REQ_USER_TITLE = HisEmployeeUtil.GetTitle(serviceReq.REQUEST_LOGINNAME);
                }
                else
                {
                    expMest.SERVICE_REQ_ID = serviceReq.ID;
                    expMest.REQ_ROOM_ID = serviceReq.REQUEST_ROOM_ID;
                }
                expMest.TDL_PATIENT_ADDRESS = serviceReq.TDL_PATIENT_ADDRESS;
                expMest.TDL_PATIENT_ID = serviceReq.TDL_PATIENT_ID;
                expMest.TDL_PATIENT_CODE = serviceReq.TDL_PATIENT_CODE;
                expMest.TDL_PATIENT_DOB = serviceReq.TDL_PATIENT_DOB;
                expMest.TDL_PATIENT_FIRST_NAME = serviceReq.TDL_PATIENT_FIRST_NAME;
                expMest.TDL_PATIENT_GENDER_ID = serviceReq.TDL_PATIENT_GENDER_ID;
                expMest.TDL_PATIENT_GENDER_NAME = serviceReq.TDL_PATIENT_GENDER_NAME;
                expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = serviceReq.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                expMest.TDL_PATIENT_LAST_NAME = serviceReq.TDL_PATIENT_LAST_NAME;
                expMest.TDL_TREATMENT_CODE = serviceReq.TDL_TREATMENT_CODE;
                expMest.TDL_PATIENT_NAME = serviceReq.TDL_PATIENT_NAME;
                expMest.TDL_SERVICE_REQ_CODE = serviceReq.SERVICE_REQ_CODE;
                expMest.TDL_TREATMENT_CODE = serviceReq.TDL_TREATMENT_CODE;
                expMest.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                expMest.TDL_INTRUCTION_TIME = serviceReq.INTRUCTION_TIME;
                expMest.TDL_INTRUCTION_DATE = serviceReq.INTRUCTION_DATE;
                expMest.IS_EXECUTE_KIDNEY_PRES = serviceReq.IS_EXECUTE_KIDNEY_PRES;
                expMest.TDL_PATIENT_TYPE_ID = serviceReq.TDL_PATIENT_TYPE_ID;
                expMest.TDL_HEIN_CARD_NUMBER = serviceReq.TDL_HEIN_CARD_NUMBER;
                expMest.TDL_PATIENT_PHONE = serviceReq.TDL_PATIENT_PHONE;
                expMest.TDL_PATIENT_MOBILE = serviceReq.TDL_PATIENT_MOBILE;
                expMest.TDL_PATIENT_COMMUNE_CODE = serviceReq.TDL_PATIENT_COMMUNE_CODE;
                expMest.TDL_PATIENT_DISTRICT_CODE = serviceReq.TDL_PATIENT_DISTRICT_CODE;
                expMest.TDL_PATIENT_PROVINCE_CODE = serviceReq.TDL_PATIENT_PROVINCE_CODE;
                expMest.TDL_PATIENT_NATIONAL_NAME = serviceReq.TDL_PATIENT_NATIONAL_NAME;
                expMest.PRES_GROUP = serviceReq.PRES_GROUP;
                expMest.IS_STAR_MARK = serviceReq.IS_STAR_MARK;
                expMest.IS_HOME_PRES = serviceReq.IS_HOME_PRES;
                expMest.IS_KIDNEY = serviceReq.IS_KIDNEY;
                expMest.PRIORITY = serviceReq.PRIORITY;
                expMest.PRIORITY_TYPE_ID = serviceReq.PRIORITY_TYPE_ID;
            }

            //Su dung sau vi can xet REQUEST_ROOM_ID cho ExpMest truoc
            HisExpMestUtil.SetTdl(expMest);
        }

        public static void SetTdl(HIS_EXP_MEST expMest, HIS_VACCINATION vaccination)
        {
            if (expMest != null && vaccination != null)
            {
                expMest.VACCINATION_ID = vaccination.ID;
                expMest.REQ_ROOM_ID = vaccination.REQUEST_ROOM_ID;
                expMest.TDL_PATIENT_ADDRESS = vaccination.TDL_PATIENT_ADDRESS;
                expMest.TDL_PATIENT_ID = vaccination.PATIENT_ID;
                expMest.TDL_PATIENT_CODE = vaccination.TDL_PATIENT_CODE;
                expMest.TDL_PATIENT_DOB = vaccination.TDL_PATIENT_DOB;
                expMest.TDL_PATIENT_FIRST_NAME = vaccination.TDL_PATIENT_FIRST_NAME;
                expMest.TDL_PATIENT_GENDER_ID = vaccination.TDL_PATIENT_GENDER_ID;
                expMest.TDL_PATIENT_GENDER_NAME = vaccination.TDL_PATIENT_GENDER_NAME;
                expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = vaccination.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                expMest.TDL_PATIENT_LAST_NAME = vaccination.TDL_PATIENT_LAST_NAME;
                expMest.TDL_PATIENT_NAME = vaccination.TDL_PATIENT_NAME;
            }

            //Su dung sau vi can xet REQUEST_ROOM_ID cho ExpMest truoc
            HisExpMestUtil.SetTdl(expMest);
        }

        //Luu du thua du lieu
        public static void SetTdl(HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq)
        {
            HisExpMestUtil.SetTdl(expMest, serviceReq, false);
        }

        public static void SetTdl(HIS_EXP_MEST expMest)
        {
            if (expMest != null)
            {
                V_HIS_ROOM room = HisRoomCFG.DATA != null ? HisRoomCFG.DATA.Where(o => o.ID == expMest.REQ_ROOM_ID).FirstOrDefault() : null;
                expMest.REQ_DEPARTMENT_ID = room.DEPARTMENT_ID;
                expMest.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                expMest.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                expMest.REQ_USER_TITLE = HisEmployeeUtil.GetTitle(expMest.REQ_LOGINNAME);
            }
        }

        public static void SetTdl(HIS_EXP_MEST expMest, HIS_TREATMENT treatment)
        {
            if (expMest != null && treatment != null)
            {
                expMest.TDL_PATIENT_ADDRESS = treatment.TDL_PATIENT_ADDRESS;
                expMest.TDL_PATIENT_ID = treatment.PATIENT_ID;
                expMest.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                expMest.TDL_PATIENT_DOB = treatment.TDL_PATIENT_DOB;
                expMest.TDL_PATIENT_FIRST_NAME = treatment.TDL_PATIENT_FIRST_NAME;
                expMest.TDL_PATIENT_GENDER_ID = treatment.TDL_PATIENT_GENDER_ID;
                expMest.TDL_PATIENT_GENDER_NAME = treatment.TDL_PATIENT_GENDER_NAME;
                expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = treatment.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                expMest.TDL_PATIENT_LAST_NAME = treatment.TDL_PATIENT_LAST_NAME;
                expMest.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;
                expMest.TDL_PATIENT_MOBILE = treatment.TDL_PATIENT_MOBILE;
                expMest.TDL_PATIENT_PHONE = treatment.TDL_PATIENT_PHONE;
                expMest.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                expMest.TDL_TREATMENT_ID = treatment.ID;
                expMest.TDL_PATIENT_TYPE_ID = treatment.TDL_PATIENT_TYPE_ID;
                expMest.TDL_HEIN_CARD_NUMBER = treatment.TDL_HEIN_CARD_NUMBER;
                expMest.TDL_PATIENT_COMMUNE_CODE = treatment.TDL_PATIENT_COMMUNE_CODE;
                expMest.TDL_PATIENT_DISTRICT_CODE = treatment.TDL_PATIENT_DISTRICT_CODE;
                expMest.TDL_PATIENT_PROVINCE_CODE = treatment.TDL_PATIENT_PROVINCE_CODE;
                expMest.TDL_PATIENT_NATIONAL_NAME = treatment.TDL_PATIENT_NATIONAL_NAME;
            }
        }

        /// <summary>
        /// Danh STT xuat thuoc theo khoa voi phieu linh, don phong kham doi voi thuoc dac biet (thuoc gay nghien/huong than, thuoc doc)
        /// </summary>
        /// <param name="medicineSpecialType"></param>
        /// <param name="requestDepartmentId"></param>
        /// <param name="createYear"></param>
        /// <returns></returns>
        public static long? GetNextSpeciaMedicineTypeNumOrder(long? medicineSpecialType, long requestDepartmentId, long createYear, long mediStockId)
        {
            if (!medicineSpecialType.HasValue)
            {
                return null;
            }
            else
            {
                string sql = "SELECT MAX(SPECIAL_MEDICINE_NUM_ORDER) FROM HIS_EXP_MEST WHERE SPECIAL_MEDICINE_TYPE = :param1 AND REQ_DEPARTMENT_ID = :param2 AND VIR_CREATE_YEAR = :param3 AND MEDI_STOCK_ID = :param4";
                long? maxNumOrder = DAOWorker.SqlDAO.GetSqlSingle<long?>(sql, medicineSpecialType.Value, requestDepartmentId, createYear, mediStockId);
                return maxNumOrder.HasValue ? maxNumOrder.Value + 1 : 1;
            }
        }

    }
}
