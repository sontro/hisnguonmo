using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXml.Config
{
    class HisConfigCFG
    {
        public const string MRS_HIS_REPORT_BHYT_NDS_ICD_CODE__OTHER = "MRS.HIS_REPORT_BHYT_NDS_ICD_CODE__OTHER";
        public const string MRS_HIS_REPORT_BHYT_NDS_ICD_CODE__TE = "MRS.HIS_REPORT_BHYT_NDS_ICD_CODE__TE";

        internal static string GetValue(string key)
        {
            try
            {
                return HisConfigs.Get<string>(key);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return "";
        }

        internal static List<string> GetListValue(string key)
        {
            try
            {
                return HisConfigs.Get<List<string>>(key);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }
    }
}
