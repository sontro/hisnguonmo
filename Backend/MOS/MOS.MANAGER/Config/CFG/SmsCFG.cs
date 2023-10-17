using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class SmsCFG
    {
        private const string MERCHANT_CODE_CFG = "MOS.SMS.MERCHANT_CODE";
        private const string SECURITY_CODE_CFG = "MOS.SMS.SECURITY_CODE";
        private const string SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_CODE_CFG = "MOS.SMS.SUBCLINICAL_RESULT_NOTIFY.REQUEST_ROOM_CODE";
        private const string SUBCLINICAL_RESULT_NOTIFY_START_TIME_CFG = "MOS.SMS.SUBCLINICAL_RESULT_NOTIFY.START_TIME";
        private const string SUBCLINICAL_RESULT_NOTIFY_DAY_NUM_CFG = "MOS.SMS.SUBCLINICAL_RESULT_NOTIFY.DAY_NUM";
        private const string SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT_CFG = "MOS.SMS.SUBCLINICAL_RESULT_NOTIFY.MESSAGE_FORMAT";
        private const string SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT_CFG = "MOS.SMS.SUBCLINICAL_RESULT_NOTIFY.RESULTING_MESSAGE_FORMAT";

        public const string RESULT_NOTIFY_MESSAGE_FORMAT_KEY__PATIENT_NAME = "<#PATIENT_NAME;>";
        public const string RESULT_NOTIFY_MESSAGE_FORMAT_KEY__AGE = "<#AGE;>";
        public const string RESULT_NOTIFY_MESSAGE_FORMAT_KEY__SERVICE_REQ_TYPE_NAME = "<#SERVICE_REQ_TYPE_NAME;>";
        public const string RESULT_NOTIFY_MESSAGE_FORMAT_KEY__EXECUTE_ROOM_NAME = "<#EXECUTE_ROOM_NAME;>";
        public const string RESULT_NOTIFY_MESSAGE_FORMAT_KEY__RESULTING_ORDER = "<#RESULTING_ORDER;>";

        private static string merchantCode;
        internal static string MERCHANT_CODE
        {
            get
            {
                if (String.IsNullOrWhiteSpace(merchantCode))
                {
                    merchantCode = ConfigUtil.GetStrConfig(MERCHANT_CODE_CFG);
                }
                return merchantCode;
            }
        }

        private static string securityCode;
        internal static string SECURITY_CODE
        {
            get
            {
                if (String.IsNullOrWhiteSpace(securityCode))
                {
                    securityCode = ConfigUtil.GetStrConfig(SECURITY_CODE_CFG);
                }
                return securityCode;
            }
        }

        private static string subclinicalResultNotifyMessageFormat;
        internal static string SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT
        {
            get
            {
                if (String.IsNullOrWhiteSpace(subclinicalResultNotifyMessageFormat))
                {
                    subclinicalResultNotifyMessageFormat = ConfigUtil.GetStrConfig(SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT_CFG);
                }
                return subclinicalResultNotifyMessageFormat;
            }
        }

        private static string subclinicalResultNotifyResultingMessageFormat;
        internal static string SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT
        {
            get
            {
                if (String.IsNullOrWhiteSpace(subclinicalResultNotifyResultingMessageFormat))
                {
                    subclinicalResultNotifyResultingMessageFormat = ConfigUtil.GetStrConfig(SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT_CFG);
                }
                return subclinicalResultNotifyResultingMessageFormat;
            }
        }

        private static List<long> subclinicalResultNotifyRequestRoomIds;
        public static List<long> SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_IDS
        {
            get
            {
                if (subclinicalResultNotifyRequestRoomIds == null)
                {
                    subclinicalResultNotifyRequestRoomIds = GetIds(SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_CODE_CFG);
                }
                return subclinicalResultNotifyRequestRoomIds;
            }
        }

        private static int? subclinicalResultNotifyDayNum;
        public static int SUBCLINICAL_RESULT_NOTIFY_DAY_NUM
        {
            get
            {
                if (!subclinicalResultNotifyDayNum.HasValue)
                {
                    subclinicalResultNotifyDayNum = ConfigUtil.GetIntConfig(SUBCLINICAL_RESULT_NOTIFY_DAY_NUM_CFG);
                }
                return subclinicalResultNotifyDayNum.Value;
            }
        }

        private static long? subclinicalResultNotifyStartTime;
        public static long SUBCLINICAL_RESULT_NOTIFY_START_TIME
        {
            get
            {
                if (!subclinicalResultNotifyStartTime.HasValue)
                {
                    subclinicalResultNotifyStartTime = ConfigUtil.GetLongConfig(SUBCLINICAL_RESULT_NOTIFY_START_TIME_CFG);
                }
                return subclinicalResultNotifyStartTime.Value;
            }
        }

        private static List<long> GetIds(string code)
        {
            List<long> result = null;
            try
            {
                List<string> codes = ConfigUtil.GetStrConfigs(code);
                if (codes != null)
                {
                    result = HisRoomCFG.DATA.Where(o => codes.Contains(o.ROOM_CODE)).Select(o => o.ID).ToList();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        internal const string RESPONSE_CODE__SUCCESS = "00";

        public static void Reload()
        {
            securityCode = ConfigUtil.GetStrConfig(SECURITY_CODE_CFG);
            merchantCode = ConfigUtil.GetStrConfig(MERCHANT_CODE_CFG);
            subclinicalResultNotifyRequestRoomIds = GetIds(SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_CODE_CFG);
            subclinicalResultNotifyDayNum = ConfigUtil.GetIntConfig(SUBCLINICAL_RESULT_NOTIFY_DAY_NUM_CFG);
            subclinicalResultNotifyStartTime = ConfigUtil.GetLongConfig(SUBCLINICAL_RESULT_NOTIFY_START_TIME_CFG);
            subclinicalResultNotifyMessageFormat = ConfigUtil.GetStrConfig(SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT_CFG);
            subclinicalResultNotifyResultingMessageFormat = ConfigUtil.GetStrConfig(SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT_CFG);
        }
    }
}
