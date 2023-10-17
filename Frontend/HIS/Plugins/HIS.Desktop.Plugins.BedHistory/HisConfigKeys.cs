using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedHistory
{
    internal class HisConfigKeys
    {
        internal const string HIS_CONFIG__PATIENT_TYPE_CODE__BHYT = "HIS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT

        internal const string HIS_CONFIG__PATIENT_TYPE_CODE__VP = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";

        internal const string CONFIG_KEY__MOS_HIS_SERE_SERV_IS_SET_PRIMARY_PATIENT_TYPE = "MOS.HIS_SERE_SERV.IS_SET_PRIMARY_PATIENT_TYPE";

        internal static string BedServiceType_NotAllow_For_OutPatient = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.Desktop.Plugins.AssignService.BedServiceType_NotAllow_For_OutPatient");

        internal const string CONFIG_KEY__BHYT__EXCEED_DAY_ALLOW_FOR_IN_PATIENT = "MOS.BHYT.EXCEED_DAY_ALLOW_FOR_IN_PATIENT";
        internal const string CONFIG_KEY__MOS__HIS_SERE_SERV__IS__USING_BED_TEMP = "MOS.HIS_SERE_SERV.IS_USING_BED_TEMP";
        internal const string CONFIG_KEY__WarningOverTotalPatientPrice__IsCheck = "HIS.Desktop.WarningOverTotalPatientPrice__IsCheck";
    }
}
