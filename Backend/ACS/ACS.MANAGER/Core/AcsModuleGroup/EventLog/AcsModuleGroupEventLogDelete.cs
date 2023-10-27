using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.EventLog
{
    class AcsModuleGroupEventLogDelete
    {
        internal static void Log(object data)
        {
            try
            {
                //ACS.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.AcsModuleGroupEventLogDelete, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
