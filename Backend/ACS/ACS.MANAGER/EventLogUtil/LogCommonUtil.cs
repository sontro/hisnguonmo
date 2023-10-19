using ACS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    class LogCommonUtil
    {
        internal static string GetEventLogContent(EventLog.Enum logEnum)
        {
            EventLog eventLog = ACS.LibraryEventLog.DatabaseEventLog.Get("VI", logEnum);
            return eventLog != null ? eventLog.message : null;
        }
    }
}
