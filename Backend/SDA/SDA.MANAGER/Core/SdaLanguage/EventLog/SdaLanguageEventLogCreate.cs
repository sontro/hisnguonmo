using System;

namespace SDA.MANAGER.Core.SdaLanguage.EventLog
{
    class SdaLanguageEventLogCreate
    {
        internal static void Log(object data)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaLanguageEventLogCreate, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
