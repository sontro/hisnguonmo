using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaExamPresCreate.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";
        private const string CONFIG_KEY_ALLOW_MOBA_EXAM_PRES = "HIS_EXP_MEST.EXP_MEST_TYPE.EXAM_PRES.IS_ALLOW_MOBA";

        internal static long PatientTypeId__BHYT;
        internal static bool ALLOW_MOBA_EXAM_PRES;

        private static string GetValue(string key)
        {
            try
            {
                return HisConfigs.Get<string>(key);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return "";
        }


        internal static void LoadConfig()
        {
            try
            {
                ALLOW_MOBA_EXAM_PRES = GetValue(CONFIG_KEY_ALLOW_MOBA_EXAM_PRES) == "1";
                PatientTypeId__BHYT = GetPatientTypeByCode(GetValue(CONFIG_KEY__PATIENT_TYPE_CODE__BHYT)).ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeByCode(string code)
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

    }
}
