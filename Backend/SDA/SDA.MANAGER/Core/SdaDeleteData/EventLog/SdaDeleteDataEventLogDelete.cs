using System;

namespace SDA.MANAGER.Core.SdaDeleteData.EventLog
{
    class SdaDeleteDataEventLogDelete
    {
        internal static void Log(object data)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaDeleteDataEventLogDelete, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
