using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignServiceEdit.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__Icd_Service_Has_Check = "HIS.HIS_ICD_SERVICE.HAS_CHECK";
        private const string CONFIG_KEY__Icd_Service_Allow_Update = "HIS.HIS_ICD_SERVICE.ALLOW_UPDATE";

        private const string CONFIG_KEY__IS_SET_PRIMARY_PATIENT_TYPE = "MOS.HIS_SERE_SERV.IS_SET_PRIMARY_PATIENT_TYPE";
        private const string CONFIG_KEY__SERVICE_REQ__IS_SERE_SERV_MIN_DURATION_ALERT = "HIS.Desktop.IsSereServMinDurationAlert";
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__VP = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";//Doi tuong VP

        private const string CONFIG_KEY__USING_SERVER_TIME = "MOS.IS_USING_SERVER_TIME";
        private const string CONFIG_KEY_CheckDepartmentInTimeWhenPresOrAssign = "HIS.Desktop.Plugins.IsCheckDepartmentInTimeWhenPresOrAssign";
        public static bool IsSereServMinDurationAlert { get; set; }
        internal static string IcdServiceHasCheck;
        internal static string IcdServiceAllowUpdate;
        internal static string IsSetPrimaryPatientType; 
        internal static long PatientTypeId__BHYT;
        internal static long PatientTypeId__VP;

        internal static string IsUsingServerTime;
        internal static bool IsCheckDepartmentInTimeWhenPresOrAssign;

        internal static void LoadConfig()
        {
            try
            {
                IsUsingServerTime = GetValue(CONFIG_KEY__USING_SERVER_TIME);
                IsCheckDepartmentInTimeWhenPresOrAssign = GetValue(CONFIG_KEY_CheckDepartmentInTimeWhenPresOrAssign) == "1";
                IsSereServMinDurationAlert = (GetValue(CONFIG_KEY__SERVICE_REQ__IS_SERE_SERV_MIN_DURATION_ALERT) == "1");
                IcdServiceHasCheck = GetValue(CONFIG_KEY__Icd_Service_Has_Check);
                IcdServiceAllowUpdate = GetValue(CONFIG_KEY__Icd_Service_Allow_Update);
                IsSetPrimaryPatientType = GetValue(CONFIG_KEY__IS_SET_PRIMARY_PATIENT_TYPE);
                PatientTypeId__BHYT = GetPatientTypeByCode(GetValue(CONFIG_KEY__PATIENT_TYPE_CODE__BHYT)).ID;
                PatientTypeId__VP = GetPatientTypeByCode(GetValue(CONFIG_KEY__PATIENT_TYPE_CODE__VP)).ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        static MOS.EFMODEL.DataModels.HIS_TREATMENT_TYPE GetTreatmentTypeById(long id)
        {
            MOS.EFMODEL.DataModels.HIS_TREATMENT_TYPE result = new MOS.EFMODEL.DataModels.HIS_TREATMENT_TYPE();
            try
            {
                result = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_TREATMENT_TYPE>().FirstOrDefault(o => o.ID == id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result ?? new MOS.EFMODEL.DataModels.HIS_TREATMENT_TYPE();
        }

        static MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeByCode(string code)
        {
            MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE result = new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
            try
            {
                result = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == code);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result ?? new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
        }

        private static string GetValue(string code)
        {
            string result = null;
            try
            {
                return HisConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }
    }
}
