using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AppointmentInfo.Config
{
    class HisConfigCFG
    {
        private const string APPOINTMENT_TIME_DEFAULT_KEY = "EXE.HIS_TREATMENT_END.APPOINTMENT_TIME_DEFAULT";
        private const string APPOINTMENT_TIME_KEY = "HIS.Desktop.Plugins.TreatmentFinish.APPOINTMENT_TIME";
        private const string MAX_OF_APPOINTMENT_DAYS = "MOS.HIS_TREATMENT.MAX_OF_APPOINTMENT_DAYS";
        private const string WARNING_OPTION_WHEN_EXCEEDING_MAX_OF_APPOINTMENT_DAYS = "MOS.HIS_TREATMENT.WARNING_OPTION_WHEN_EXCEEDING_MAX_OF_APPOINTMENT_DAYS";

        internal static long AppointmentTimeDefault;
        internal static string AppointmentTimeOption;
        internal static long? MaxOfAppointmentDays;
        internal static long? WarningOptionWhenExceedingMaxOfAppointmentDays;

        internal static void LoadConfig()
        {
            try
            {
                AppointmentTimeDefault = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<long>(APPOINTMENT_TIME_DEFAULT_KEY);
                AppointmentTimeOption = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(APPOINTMENT_TIME_KEY);
                try
                {
                    string maxDayStr = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(MAX_OF_APPOINTMENT_DAYS);
                    if (!String.IsNullOrWhiteSpace(maxDayStr))
                    {
                        MaxOfAppointmentDays = Convert.ToInt64(maxDayStr);
                    }
                    else
                    {
                        MaxOfAppointmentDays = null;
                    }
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                try
                {
                    string maxDayOption = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(WARNING_OPTION_WHEN_EXCEEDING_MAX_OF_APPOINTMENT_DAYS);
                    if (!String.IsNullOrWhiteSpace(maxDayOption))
                    {
                        WarningOptionWhenExceedingMaxOfAppointmentDays = Convert.ToInt64(maxDayOption);
                    }
                    else
                    {
                        WarningOptionWhenExceedingMaxOfAppointmentDays = null;
                    }
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
