using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentLockFee
{
    public class HisTreatmentFeeLockTimeCFG
    {

        private const string IS_ENABLE_CONTROL = "1";

        public static bool IsEnableControlFeeLockTime
        {
            get
            {
                return (HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HFS.HIS_TREATMENT.FEE_LOCK_TIME_ENABLE_CONTROL") == IS_ENABLE_CONTROL);
            }
        }
    }
}
