using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Camera.Config
{
    class AppConfigKeys
    {
        internal const string CONFIG_KEY__MODULE_CAMERA__SHORTCUT_KEY = "CONFIG_KEY__MODULE_CAMERA__SHORTCUT_KEY";
        internal const string CONFIG_KEY__Camera__IsSavingInLocal = "HIS.Desktop.Plugins.Camera.IsSavingInLocal";

        internal static bool IsSavingInLocal
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__Camera__IsSavingInLocal) == "1";
            }
        }
    }
}
