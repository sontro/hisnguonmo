using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT.Config
{
    class HisConfigCFG
    {
        public const string MRS_HIS_REPORT_BHYT_NDS_ICD_CODE__OTHER = "MRS.HIS_REPORT_BHYT_NDS_ICD_CODE__OTHER";
        public const string MRS_HIS_REPORT_BHYT_NDS_ICD_CODE__TE = "MRS.HIS_REPORT_BHYT_NDS_ICD_CODE__TE";
        public const string MOS_HIS_HEIN_APPROVAL__IS_AUTO_EXPORT_XML = "MOS.HIS_HEIN_APPROVAL.IS_AUTO_EXPORT_XML";
        public const string MOS__BHYT__CALC_MATERIAL_PACKAGE_PRICE_OPTION = "MOS.BHYT.CALC_MATERIAL_PACKAGE_PRICE_OPTION";
        public const string XML__4210__MATERIAL_PRICE_OPTION = "XML.EXPORT.4210.MATERIAL_PRICE_OPTION";

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
