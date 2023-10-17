using System;

namespace SDA.MANAGER.Core.SdaNotify.EventLog
{
    class SdaNotifyEventLogUnLock
    {
        internal static void Log(object data)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaNotifyEventLogUnLock, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
