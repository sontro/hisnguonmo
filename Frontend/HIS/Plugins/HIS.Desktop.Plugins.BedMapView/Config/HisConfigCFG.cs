using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedMapView.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__SHOW_INFO_OPTION = "HIS.DESKTOP.HIS_BED_MAP.SHOW_INFO_OPTION";
        internal static string SHOW_INFO_OPTION;

        internal static void LoadConfig()
        {
            try
            {
                SHOW_INFO_OPTION = GetValue(CONFIG_KEY__SHOW_INFO_OPTION);
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
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }


    }
}
