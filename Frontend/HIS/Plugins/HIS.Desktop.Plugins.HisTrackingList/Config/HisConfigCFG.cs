using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTrackingList.Config
{
    class HisConfigCFG
    {
        internal static string Config_TrackingCreate_BloodPresOption
        {
            get
            {
                var result = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(ConfigKeyss.DBCODE__HIS_DESKTOP_PLUGINS_TRACKING_CREATE_BLOOD_PRES_OPTION);
                return result;
            }
        }
    }
}
