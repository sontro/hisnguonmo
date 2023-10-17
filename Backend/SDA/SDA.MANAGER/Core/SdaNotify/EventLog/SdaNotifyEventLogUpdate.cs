using System;

namespace SDA.MANAGER.Core.SdaNotify.EventLog
{
    class SdaNotifyEventLogUpdate
    {
        internal static void Log(object beforeData, object afterData)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaNotifyEventLogUpdate, Newtonsoft.Json.JsonConvert.SerializeObject(beforeData), Newtonsoft.Json.JsonConvert.SerializeObject(afterData));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
