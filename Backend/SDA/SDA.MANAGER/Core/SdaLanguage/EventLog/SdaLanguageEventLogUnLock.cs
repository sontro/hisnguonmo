using System;

namespace SDA.MANAGER.Core.SdaLanguage.EventLog
{
    class SdaLanguageEventLogUnLock
    {
        internal static void Log(object data)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaLanguageEventLogUnLock, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
