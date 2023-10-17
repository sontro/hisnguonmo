using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedRoomWithIn
{
    class Config
    {
        private const string SHOW_PRIMARY_PATIENT_TYPE = "MOS.HIS_SERE_SERV.IS_SET_PRIMARY_PATIENT_TYPE";
        private const string HIS_CONFIG__PATIENT_TYPE_CODE__BHYT = "HIS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT
        private const string HIS_CONFIG__PATIENT_TYPE_CODE__VP = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";
        private const string USING_BED_TEMP = "MOS.HIS_SERE_SERV.IS_USING_BED_TEMP";
        private const string CONFIG_KEY__IS_MANUAL_IN_CODE = "MOS.HIS_TREATMENT.IS_MANUAL_IN_CODE";
        private const string IsRequiredChooseRoomCFG = "HIS.Desktop.Plugins.DepartmentTranReceive.IsRequiredChooseRoom";
        private const string IsPatientClassifyOption = "HIS.Desktop.Plugins.PatientClassifyOption";
        public static bool IsPatientClassify
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(IsPatientClassifyOption) == "1";
            }
        }
        public static string IsRequiredChooseRoom
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(IsRequiredChooseRoomCFG);
            }
        }

        public static string IsPrimaryPatientType
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SHOW_PRIMARY_PATIENT_TYPE);
            }
        }

        public static bool IsManualInCode
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__IS_MANUAL_IN_CODE) == "1";
            }
        }

        public static string IsUsingBedTemp
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(USING_BED_TEMP);
            }
        }

        public static long PatientTypeId__BHYT
        {
            get
            {
                var patientType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(HIS_CONFIG__PATIENT_TYPE_CODE__BHYT));
                if (patientType != null)
                {
                    return patientType.ID;
                }
                else
                    return -1;
            }
        }

        public static long PatientTypeId__VP
        {
            get
            {
                var patientType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(HIS_CONFIG__PATIENT_TYPE_CODE__VP));
                if (patientType != null)
                {
                    return patientType.ID;
                }
                else
                    return -1;
            }
        }
    }
}
