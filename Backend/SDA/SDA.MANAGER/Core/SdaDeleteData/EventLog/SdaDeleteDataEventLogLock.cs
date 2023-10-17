using System;

namespace SDA.MANAGER.Core.SdaDeleteData.EventLog
{
    class SdaDeleteDataEventLogLock
    {
        internal static void Log(object data)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaDeleteDataEventLogLock, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
