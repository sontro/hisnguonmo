using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class HisTreatmentFeeLockTimeCFG
    {

        private const string IS_ENABLE_CONTROL = "1";

        public static bool IsEnableControlFeeLockTime
        {
            get
            {
                return (SdaConfigs.Get<string>(ExtensionConfigKey.HIS_TREATMENT__FEE_LOCK_TIME) == IS_ENABLE_CONTROL);
            }
        }
    }
}
