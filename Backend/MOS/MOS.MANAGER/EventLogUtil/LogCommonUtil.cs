using MOS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class LogCommonUtil
    {
        internal static string GetEventLogContent(EventLog.Enum logEnum)
        {
            EventLog eventLog = MOS.LibraryEventLog.DatabaseEventLog.Get("VI", logEnum);
            return eventLog != null ? eventLog.message : null;
        }
    }
}
