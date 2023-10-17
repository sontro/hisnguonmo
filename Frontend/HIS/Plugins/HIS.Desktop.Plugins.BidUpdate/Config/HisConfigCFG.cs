using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;
using HIS.Desktop.LocalStorage.HisConfig;

namespace HIS.Desktop.Plugins.BidUpdate.Config
{
    class HisConfigCFG
    {
        private const string IS_SET_BHYT_INFO_FROM_TYPE_BY_DEFAULT = "MOS.HIS_MEDICINE.IS_SET_BHYT_INFO_FROM_TYPE_BY_DEFAULT";
        internal static bool IsSet__BHYT;

        internal static void LoadConfig()
        {
            try
            {
                LogSystem.Debug("LoadConfig => 1");
                IsSet__BHYT = GetValue(IS_SET_BHYT_INFO_FROM_TYPE_BY_DEFAULT) == "1";
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
    }
}
