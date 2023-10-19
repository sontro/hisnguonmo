using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.EventLog
{
    class AcsModuleGroupEventLogUnLock
    {
        internal static void Log(object data)
        {
            try
            {
                //ACS.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.AcsModuleGroupEventLogUnLock, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
