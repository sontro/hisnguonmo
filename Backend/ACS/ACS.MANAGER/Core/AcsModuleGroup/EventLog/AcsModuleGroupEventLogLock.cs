using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.EventLog
{
    class AcsModuleGroupEventLogLock
    {
        internal static void Log(object data)
        {
            try
            {
                //ACS.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.AcsModuleGroupEventLogLock, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
