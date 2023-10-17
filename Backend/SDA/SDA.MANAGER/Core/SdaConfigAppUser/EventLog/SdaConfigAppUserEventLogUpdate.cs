using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser.EventLog
{
    class SdaConfigAppUserEventLogUpdate
    {
        internal static void Log(object beforeData, object afterData)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaConfigAppUserEventLogUpdate, Newtonsoft.Json.JsonConvert.SerializeObject(beforeData), Newtonsoft.Json.JsonConvert.SerializeObject(afterData));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
