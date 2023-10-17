using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HisHeinApprovalCFG
    {
        private const string XML_FOLDER_PATH_CFG = "MOS.HIS_HEIN_APPROVAL.XML_FOLDER_PATH";
        private const string IS_AUTO_EXPORT_XML_CFG = "MOS.HIS_HEIN_APPROVAL.IS_AUTO_EXPORT_XML";
        private const string XML4210_FOLDER_PATH_CFG = "MOS.HIS_HEIN_APPROVAL.XML4210_FOLDER_PATH";
        private const string AUTO_XML_BHYT_PREFIX_RESTRICT = "MOS.HIS_HEIN_APPROVAL.AUTO_EXPORT_XML.BHYT_PREFIX_RESTRICT";
        private const string NOT_AUTO_EXPORT_XML_NO_EXAM_CFG = "MOS.EXPORT_XML.NOT_AUTO_EXPORT_XML_NO_EXAM";
        private const string SYNC_XML_FPT_OPTION_CFG = "MOS.HIS_HEIN_APPROVAL.SYNC_XML_FPT_OPTION";

        private static bool? notAutoExportXmlNoExam;
        public static bool NOT_AUTO_EXPORT_XML_NO_EXAM
        {
            get
            {
                if (!notAutoExportXmlNoExam.HasValue)
                {
                    notAutoExportXmlNoExam = ConfigUtil.GetIntConfig(NOT_AUTO_EXPORT_XML_NO_EXAM_CFG) == 1;
                }
                return notAutoExportXmlNoExam.Value;
            }
        }

        private static bool? isAutoExportXml;
        public static bool IS_AUTO_EXPORT_XML
        {
            get
            {
                if (!isAutoExportXml.HasValue)
                {
                    isAutoExportXml = ConfigUtil.GetIntConfig(IS_AUTO_EXPORT_XML_CFG) == 1;
                }
                return isAutoExportXml.Value;
            }
        }

        private static string xmlFolderPath;
        public static string XML_FOLDER_PATH
        {
            get
            {
                if (xmlFolderPath == null)
                {
                    xmlFolderPath = ConfigUtil.GetStrConfig(XML_FOLDER_PATH_CFG);
                }
                return xmlFolderPath;
            }
        }

        private static string xml4210FolderPath;
        public static string XML4210_FOLDER_PATH
        {
            get
            {
                if (xml4210FolderPath == null)
                {
                    xml4210FolderPath = ConfigUtil.GetStrConfig(XML4210_FOLDER_PATH_CFG);
                }
                return xml4210FolderPath;
            }
        }

        private static List<string> heinCardNumberPrefixRestricts;
        public static List<string> HEIN_CARD_NUMBER_PREFIX_RESTRICTS
        {
            get
            {
                if (heinCardNumberPrefixRestricts == null)
                {
                    heinCardNumberPrefixRestricts = ConfigUtil.GetStrConfigs(AUTO_XML_BHYT_PREFIX_RESTRICT);
                }
                return heinCardNumberPrefixRestricts;
            }
        }

        private static string syncXmlFptOption;
        public static string SYNC_XML_FPT_OPTION
        {
            get
            {
                if (syncXmlFptOption == null)
                {
                    syncXmlFptOption = ConfigUtil.GetStrConfig(SYNC_XML_FPT_OPTION_CFG);
                }
                return syncXmlFptOption;
            }
        }

        public static void Reload()
        {
            var autoExportXml = ConfigUtil.GetIntConfig(IS_AUTO_EXPORT_XML_CFG) == 1;
            var folderPath = ConfigUtil.GetStrConfig(XML_FOLDER_PATH_CFG);
            var restricts = ConfigUtil.GetStrConfigs(AUTO_XML_BHYT_PREFIX_RESTRICT);
            notAutoExportXmlNoExam = ConfigUtil.GetIntConfig(NOT_AUTO_EXPORT_XML_NO_EXAM_CFG) == 1;
            isAutoExportXml = autoExportXml;
            xmlFolderPath = folderPath;
            heinCardNumberPrefixRestricts = restricts;
            syncXmlFptOption = ConfigUtil.GetStrConfig(SYNC_XML_FPT_OPTION_CFG);
            MOS.ApiConsumerManager.ApiConsumerStore.hxtConsumerWrapper = null;
            isAutoExportXml = ConfigUtil.GetIntConfig(IS_AUTO_EXPORT_XML_CFG) == 1;
        }
    }
}
