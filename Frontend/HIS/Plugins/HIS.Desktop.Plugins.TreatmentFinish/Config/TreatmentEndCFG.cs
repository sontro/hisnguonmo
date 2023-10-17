using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.Config
{
    class TreatmentEndCFG
    {
        internal const string TREATMENT_END___APPOINTMENT_TIME_DEFAULT_KEY = "EXE.HIS_TREATMENT_END.APPOINTMENT_TIME_DEFAULT";
        internal const string PRESCRIPTION_TIME_AND_APPOINTMENT_TIME_KEY = "HIS.Desktop.Plugins.TreatmentFinish.APPOINTMENT_TIME";
        const string isPrescription = "1";

        internal static long treatmentEndAppointmentTimeDefault;
        internal static bool AppointmentTimeDefault;

        internal static void GetConfig()
        {
            try
            {
                treatmentEndAppointmentTimeDefault = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<long>(TREATMENT_END___APPOINTMENT_TIME_DEFAULT_KEY);
                AppointmentTimeDefault = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(PRESCRIPTION_TIME_AND_APPOINTMENT_TIME_KEY) == isPrescription;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
