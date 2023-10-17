using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.AlertHospitalFeeNotBHYT
{
    class ConfigCFG
    {
        private const string CONFIG_KEY__IS_ALERT_HOSPITALFEE_NOTBHYT = "HIS.Desktop.Plugins.Library.AlertHospitalFeeNotBHYT";
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT

        internal const string valueString__true = "1";
        internal const int valueInt__true = 1;

        internal static long PatientTypeId__BHYT;
        internal static bool IsAlertHospitalFeeNotBHYT;

        internal static void LoadConfig()
        {
            PatientTypeId__BHYT = GetPatientTypeByCode(GetValue(CONFIG_KEY__PATIENT_TYPE_CODE__BHYT)).ID;
            IsAlertHospitalFeeNotBHYT = (GetValue(CONFIG_KEY__IS_ALERT_HOSPITALFEE_NOTBHYT) == valueString__true);
        }

        static MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeByCode(string code)
        {
            MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE result = new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
            try
            {
                result = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE.ToLower() == code.ToLower().Trim());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result ?? new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
        }

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
    }
}
