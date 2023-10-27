using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.EventLog
{
    class AcsModuleGroupEventLogUpdate
    {
        internal static void Log(object beforeData, object afterData)
        {
            try
            {
                //ACS.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.AcsModuleGroupEventLogUpdate, Newtonsoft.Json.JsonConvert.SerializeObject(beforeData), Newtonsoft.Json.JsonConvert.SerializeObject(afterData));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
