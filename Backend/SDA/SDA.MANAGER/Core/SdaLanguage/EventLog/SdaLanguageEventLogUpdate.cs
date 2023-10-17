using System;

namespace SDA.MANAGER.Core.SdaLanguage.EventLog
{
    class SdaLanguageEventLogUpdate
    {
        internal static void Log(object beforeData, object afterData)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaLanguageEventLogUpdate, Newtonsoft.Json.JsonConvert.SerializeObject(beforeData), Newtonsoft.Json.JsonConvert.SerializeObject(afterData));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
