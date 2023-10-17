using HIS.Desktop.LocalStorage.ConfigApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk.Config
{
    internal class AppConfigs
    {
        public static long CheDoTuDongCheckThongTinTheBHYT { get; set; }
        public static int SocialInsuranceType { get; set; }

        private const string CONFIG_KEY__HIS_DESKTOP__PLUGINS_AUTO_CHECK_HEIN_DATE_TO = "CONFIG_KEY__HIS_DESKTOP__PLUGINS_AUTO_CHECK_HEIN_DATE_TO";
        private const string CONFIG_KEY__HIS_DESKTOP__REGISTER__SOCIAL_INSURANCE_TYPE = "CONFIG_KEY__HIS_DESKTOP__REGISTER__SOCIAL_INSURANCE_TYPE";

        public static void LoadConfig()
        {
            try
            {
                SocialInsuranceType = ConfigApplicationWorker.Get<int>(CONFIG_KEY__HIS_DESKTOP__REGISTER__SOCIAL_INSURANCE_TYPE);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            try
            {
                CheDoTuDongCheckThongTinTheBHYT = ConfigApplicationWorker.Get<long>(CONFIG_KEY__HIS_DESKTOP__PLUGINS_AUTO_CHECK_HEIN_DATE_TO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
