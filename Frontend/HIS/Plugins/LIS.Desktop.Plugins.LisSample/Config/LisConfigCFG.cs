using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.LisConfig;

namespace LIS.Desktop.Plugins.LisSample.Config
{
    class LisConfigCFG
    {
        private const string CONFIG_KEY__IS_AUTO_CREATE_BARCODE = "LIS.LIS_SAMPLE.IS_AUTO_CREATE_BARCODE";
        private const string CONFIG_KEY__PRINT_BARCODE_BY_BARTENDER = "LIS.LIS_SAMPLE.PRINT_BARCODE.BY_BARTENDER";
        private const string CONFIG_KEY__LENGTH_BARCODE = "LIS.LIS_SAMPLE.BARCODE_LENGTH";

        internal static string IS_AUTO_CREATE_BARCODE;
        internal static string PRINT_BARCODE_BY_BARTENDER;
        internal static string LENGTH_BARCODE;

        internal static void LoadConfig()
        {
            try
            {
                LENGTH_BARCODE = GetValue(CONFIG_KEY__LENGTH_BARCODE);
                IS_AUTO_CREATE_BARCODE = GetValue(CONFIG_KEY__IS_AUTO_CREATE_BARCODE);
                PRINT_BARCODE_BY_BARTENDER = GetValue(CONFIG_KEY__PRINT_BARCODE_BY_BARTENDER);
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
                return LisConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
               Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }
    }
}
