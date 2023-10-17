using System;

namespace SDA.MANAGER.Core.SdaTranslate.EventLog
{
    class SdaTranslateEventLogUpdate
    {
        internal static void Log(object beforeData, object afterData)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaTranslateEventLogUpdate, Newtonsoft.Json.JsonConvert.SerializeObject(beforeData), Newtonsoft.Json.JsonConvert.SerializeObject(afterData));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
