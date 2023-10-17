using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD4210.Config
{
    class HisConfigCFG
    {
        public const string MRS_HIS_REPORT_BHYT_NDS_ICD_CODE__OTHER = "MRS.HIS_REPORT_BHYT_NDS_ICD_CODE__OTHER";
        public const string MRS_HIS_REPORT_BHYT_NDS_ICD_CODE__TE = "MRS.HIS_REPORT_BHYT_NDS_ICD_CODE__TE";
        public const string MOS_HIS_HEIN_APPROVAL__IS_AUTO_EXPORT_XML = "MOS.HIS_HEIN_APPROVAL.IS_AUTO_EXPORT_XML";
        public const string MOS__BHYT__CALC_MATERIAL_PACKAGE_PRICE_OPTION = "MOS.BHYT.CALC_MATERIAL_PACKAGE_PRICE_OPTION";
        public const string XML__4210__MATERIAL_PRICE_OPTION = "XML.EXPORT.4210.MATERIAL_PRICE_OPTION";
        public const string XML__4210__MATERIAL_STENT_RATIO_OPTION = "XML.EXPORT.4210.MATERIAL_STENT_RATIO_OPTION";
        public const string TEN_BENH_OPTION = "HIS.Desktop.Plugins.ExportXml.TenBenhOption";
        public const string MaThuocOption = "HIS.Desktop.Plugins.ExportXml.HeinServiceTypeCodeNoTutorial";
        public const string XmlNumbers = "HIS.Desktop.Plugins.ExportXml.XmlNumbers";
        public const string Stent2LimitOption = "XML.EXPORT.4210.MATERIAL_STENT2_LIMIT_OPTION";
        private const string AUTO_LOAD_FILE__DAY_NUMBER = "HIS.DESKTOP.XML4210.AUTO_LOAD_FILE.DAY_NUMBER";
        private const string AUTO_EXPORT_XML = "MOS.HIS_HEIN_APPROVAL.IS_AUTO_EXPORT_XML";
        internal const string IS_TREATMENT_DAY_COUNT_6556 = "XML.EXPORT.4210.IS_TREATMENT_DAY_COUNT_6556";
        internal const string MaBacSiOption = "XML.EXPORT.4210.XML3.MA_BAC_SI_EXAM_OPTION";
        internal const string NgayYlenhOption = "XML.EXPORT.4210.XML3.NGAY_YL_OPTION";

        internal static int AutoLoadFileDayNumber;
        internal static bool IsAutoExportXml;

        internal static void LoadConfig()
        {
            try
            {
                LogSystem.Debug("LoadConfig => 1");
                AutoLoadFileDayNumber = GetIntValue(AUTO_LOAD_FILE__DAY_NUMBER);
                IsAutoExportXml = GetValue(AUTO_EXPORT_XML) == "1"; ;
                LogSystem.Debug("LoadConfig => 2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

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

        internal static int GetIntValue(string key)
        {
            try
            {
                return HisConfigs.Get<int>(key);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return 0;
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
