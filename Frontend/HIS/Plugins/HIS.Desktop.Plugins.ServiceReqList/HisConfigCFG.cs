using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__OLD_SYSTEM_INTEGRATION_TYPE = "MOS.OLD_SYSTEM.INTEGRATION_TYPE";
        private const string CONFIG_KEY__LIS_VERSION = "MOS.LIS.INTEGRATION_VERSION";
        private const string CONFIG_KEY__LIS_OPTION = "MOS.LIS.INTEGRATE_OPTION";
        private const string CONFIG_KEY__LIS_TYPE = "MOS.LIS.INTEGRATION_TYPE";
        private const string CONFIG_KEY__IsUseGetDynamic = "HIS.Desktop.Plugins.IsUseGetDynamic";
        internal const string CONFIG_KEY__OptionMergePrintIsmerge = "HIS.Desktop.Plugins.OptionMergePrint.Ismerge";
        private const string CONFIG_KEY__ConnectDrugInterventionInfo = "HIS.Desktop.Plugins.AssignPrescription.ConnectDrugInterventionInfo";
        private const string CONFIG_KEY__ShowPresAmount = "HIS.Desktop.Plugins.AssignPrescriptionPK.ShowPresAmount";
        private const string CONFIG_KEY__AutoDeleteEmrDocumentWhenEditReq = "HIS.Desktop.Plugins.ServiceReqList.AutoDeleteEmrDocumentWhenEditReq";


        internal static bool IsShowPresAmount;
        internal static bool IsOldSystemIntegration;
        internal static bool IsUseInventecLis;
        internal static bool IsUseGetDynamic;
        internal static bool IsmergeOptionMergePrint;
        internal static bool ConnectDrugInterventionInfo;
        internal static string AutoDeleteEmrDocumentWhenEditReq;

        internal static void LoadConfig()
        {
            try
            {
                IsmergeOptionMergePrint = GetValue(CONFIG_KEY__OptionMergePrintIsmerge) == GlobalVariables.CommonStringTrue;
                string oldSystem = GetValue(CONFIG_KEY__OLD_SYSTEM_INTEGRATION_TYPE);
                IsUseGetDynamic = GetValue(CONFIG_KEY__IsUseGetDynamic) == GlobalVariables.CommonStringTrue;
                IsOldSystemIntegration = (!String.IsNullOrWhiteSpace(oldSystem) && oldSystem != "0");
                IsUseInventecLis = CheckUserInventecLis();
                ConnectDrugInterventionInfo = GetValue(CONFIG_KEY__ConnectDrugInterventionInfo) == GlobalVariables.CommonStringTrue;
                IsShowPresAmount = GetValue(CONFIG_KEY__ShowPresAmount) == GlobalVariables.CommonStringTrue;
                AutoDeleteEmrDocumentWhenEditReq = GetValue(CONFIG_KEY__AutoDeleteEmrDocumentWhenEditReq);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
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

        private static bool CheckUserInventecLis()
        {
            try
            {
                string lisVersion = HisConfigs.Get<string>(CONFIG_KEY__LIS_VERSION);
                string lisIntegrateOption = HisConfigs.Get<string>(CONFIG_KEY__LIS_OPTION);
                string lisIntegrateType = HisConfigs.Get<string>(CONFIG_KEY__LIS_TYPE);

                if (lisVersion == "1" && lisIntegrateOption == "1")
                    return true;

                if (lisVersion == "2" && lisIntegrateType == "1")
                    return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }
    }
}
