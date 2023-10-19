using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.ActivityProcessor
{
    public class ActivityLogSave
    {
        private static int countFailed = 0;
        private static bool IsRunning = false;
        public static void Run()
        {
            try
            {
                if (IsRunning)
                {
                    Inventec.Common.Logging.LogSystem.Info("Tien trinh ActivityLogSave dang chay");
                    return;
                }
                IsRunning = true;
                List<ACS_ACTIVITY_LOG> logs = ActivityLogCache.Pop();

                Inventec.Common.Logging.LogSystem.Info("ActivityLogs count: " + (logs != null ? logs.Count : 0));

                if (logs != null && logs.Count > 0)
                {
                    var result = new Core.AcsActivityLog.AcsActivityLogBO().Create(logs);
                    if (!result)
                    {
                        Inventec.Common.Logging.LogSystem.Error("Insert ACS_ACTIVITY_LOG that bai");
                        if (countFailed < 5)
                        {
                            countFailed++;
                            ActivityLogCache.Push(logs);
                        }
                        else
                        {
                            countFailed = 0;
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("ActivityLog", logs));
                        }
                    }
                    else
                    {
                        countFailed = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            IsRunning = false;
        }
    }
}
