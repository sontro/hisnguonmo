using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentIcdEdit
{
    class HisConfig
    {
        const string IS__TRUE = "1";

        private const string DefaultCheckedCheckboxIssueOutPatientStoreCodeSTR = "HIS.Desktop.Plugins.TreatmentFinish.DefaultCheckedCheckboxCreateOutPatientMediRecord";
        private const string EnableCheckboxIssueOutPatientStoreCodeSTR = "HIS.Desktop.Plugins.TreatmentFinish.EnableCheckboxCreateOutPatientMediRecord";

        private const string checkSovaovien = "MOS.HIS_TREATMENT.IS_MANUAL_IN_CODE";

        private const string SuaThongTinHoSoDieuTri = "MOS.HIS_TREATMENT.UPDATE_INFO_OPTION";


        internal static bool checkSovaovien_;
        internal static bool IsCheckedCheckboxIssueOutPatientStoreCode;
        internal static bool IsEnableCheckboxIssueOutPatientStoreCode;
        internal static bool SuaThongTinHoSoDieuTri_;


        internal static void GetConfig()
        {
            try
            {
                IsCheckedCheckboxIssueOutPatientStoreCode = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(DefaultCheckedCheckboxIssueOutPatientStoreCodeSTR) == IS__TRUE;
                IsEnableCheckboxIssueOutPatientStoreCode = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(EnableCheckboxIssueOutPatientStoreCodeSTR) == IS__TRUE;

                checkSovaovien_ = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(checkSovaovien) == IS__TRUE;
                SuaThongTinHoSoDieuTri_ = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SuaThongTinHoSoDieuTri) == IS__TRUE;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
