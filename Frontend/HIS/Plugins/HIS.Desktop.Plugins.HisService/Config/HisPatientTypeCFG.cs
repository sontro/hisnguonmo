using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisService.Config
{
    class HisPatientTypeCFG
    {
        private static long patientTypeIdIsFee;
        public static long PATIENT_TYPE_ID__IS_FEE
        {
            get
            {
                if (patientTypeIdIsFee == 0)
                {
                    patientTypeIdIsFee = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE"));
                }
                return patientTypeIdIsFee;
            }
            set
            {
                patientTypeIdIsFee = value;
            }
        }

        private static long patientTypeIdIsHein;
        internal static long PATIENT_TYPE_ID__BHYT
        {
            get
            {
                if (patientTypeIdIsHein == 0)
                {
                    patientTypeIdIsHein = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT"));
                }
                return patientTypeIdIsHein;
            }
            set
            {
                patientTypeIdIsHein = value;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                var data = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == code);
                result = data.ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Debug(ex);
                result = 0;
            }
            return result;
        }
    }
}
