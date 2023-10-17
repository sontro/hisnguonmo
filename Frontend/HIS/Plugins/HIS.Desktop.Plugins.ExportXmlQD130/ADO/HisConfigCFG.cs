using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD130.ADO
{
    class HisConfigCFG
    {
        internal const string HIS_QD_130_BYT__CONNECTION_INFO = "HIS.QD_130_BYT.CONNECTION_INFO";

        internal static string QD_130_BYT__CONNECTION_INFO;

        internal static void LoadConfig()
        {
            try
            {
                QD_130_BYT__CONNECTION_INFO = GetValue(HIS_QD_130_BYT__CONNECTION_INFO);
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
