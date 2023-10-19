using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.ActivityProcessor
{
    class ActivityLogCache
    {
        private static List<ACS_ACTIVITY_LOG> activityLogs = new List<ACS_ACTIVITY_LOG>();


        public static bool Push(ACS_ACTIVITY_LOG eventLog)
        {
            bool result = false;
            try
            {
                activityLogs.Add(eventLog);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static bool Push(List<ACS_ACTIVITY_LOG> eventLogs)
        {
            bool result = false;
            try
            {
                activityLogs.AddRange(eventLogs);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static List<ACS_ACTIVITY_LOG> Pop()
        {
            List<ACS_ACTIVITY_LOG> result = new List<ACS_ACTIVITY_LOG>();
            try
            {
                result.AddRange(activityLogs);
                activityLogs.Clear();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
