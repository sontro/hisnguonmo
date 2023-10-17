using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PrescriptionAbsentList.Config
{
    class ConfigKeys
    {
        internal const string WAITING_SCREEN__TIMER_FOR_AUTO_LOAD_PATIENTS_KEY = "EXE.WAITING_SCREEN.TIMER_FOR_AUTO_LOAD_PATIENTS";

        internal static long timerForAutoPatients;

        internal static void GetConfig()
        {
            try
            {
                timerForAutoPatients = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<long>(WAITING_SCREEN__TIMER_FOR_AUTO_LOAD_PATIENTS_KEY);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
