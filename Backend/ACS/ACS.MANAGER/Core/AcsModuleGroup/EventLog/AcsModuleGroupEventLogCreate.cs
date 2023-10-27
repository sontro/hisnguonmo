using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.EventLog
{
    class AcsModuleGroupEventLogCreate
    {
        internal static void Log(object data)
        {
            try
            {
                //ACS.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.AcsModuleGroupEventLogCreate, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
