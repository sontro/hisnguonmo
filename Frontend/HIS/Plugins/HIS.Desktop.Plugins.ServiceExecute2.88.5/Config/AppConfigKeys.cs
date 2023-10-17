using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute.Config
{
    class AppConfigKeys
    {
        internal const string CONFIG_KEY__MODULE_CAMERA__SHORTCUT_KEY = "CONFIG_KEY__MODULE_CAMERA__SHORTCUT_KEY";

        internal const string HIS_DESKTOP_SURGSERVICEREQEXECUTE_EXECUTE_ROLE_DEFAULT = "HIS.DESKTOP.PLUGINS.SURGSERVICEREQEXECUTE.EXECUTE_ROLE_DEFAULT";

        internal const string MOS__HIS_SERVICE_REQ__ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT = "MOS.HIS_SERVICE_REQ.ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT";

        internal const string IS_CHECKING_PERMISSON = "MOS.HIS_SERE_SERV_PTTT.IS_CHECKING_PERMISSON";

        internal const string CONFIG_KEY__Camera__IsSavingInLocal = "HIS.Desktop.Plugins.Camera.IsSavingInLocal";

        internal const string CONFIG_KEY__IsRequiredPtttPriority = "HIS.Desktop.Plugins.ServiceExecute.IsRequiredPtttPriority";
        internal const string CONFIG_KEY__IsInitCameraDefault = "HIS.Desktop.Plugins.ServiceExecute.IsInitCameraDefault";

        internal static bool IsInitCameraDefault
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__IsInitCameraDefault) == "1";
            }
        }

        internal static bool IsSavingInLocal
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__Camera__IsSavingInLocal) == "1";
            }
        }

        internal static string CheckPermisson
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(IS_CHECKING_PERMISSON);
            }
        }

        internal static bool IsRequiredPtttPriority
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__IsRequiredPtttPriority) == "1";
            }
        }
    }
}
