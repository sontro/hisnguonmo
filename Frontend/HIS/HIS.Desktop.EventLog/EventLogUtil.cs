using His.EventLog;
using Inventec.Common.Logging;
using System;

namespace HIS.Desktop.EventLog
{
    public class EventLogUtil
    {
        private static string lang = "VietNamese";

        public static string SetLog(His.EventLog.Message.Enum EventLogEnum)
        {
            try
            {
                string result = "";
                Message EventLog = His.EventLog.FrontendMessage.Get(lang, EventLogEnum);
                if (EventLog != null && !String.IsNullOrEmpty(EventLog.message))
                {
                    result = EventLog.message;
                }
                else
                {
                    LogSystem.Error("Thu vien chua khai bao key.");
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return "";
            }
        }

        public static string SetLog(His.EventLog.Message.Enum eventLogEnum, string[] extraMessage)
        {
            string result = "";
            try
            {
                Message EventLog = His.EventLog.FrontendMessage.Get(lang, eventLogEnum);
                if (EventLog != null && !String.IsNullOrEmpty(EventLog.message))
                {
                    result = EventLog.message;
                    try
                    {
                        result = String.Format(EventLog.message, extraMessage);
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error("Khong format duoc string.", ex);
                        result = EventLog.message;
                    }
                }
                else
                {
                    LogSystem.Error("Thu vien chua khai bao key.");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = "";
            }
            return result;
        }
    }
}
