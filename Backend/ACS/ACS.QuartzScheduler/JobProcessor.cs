//using ACS.QuartzScheduler.Hid;
//using ACS.QuartzScheduler.The;
//using ACS.QuartzScheduler.Ydt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.QuartzScheduler
{
    public class JobProcessor
    {
        private static bool isAdd = false;
        public static bool AddJob()
        {
            try
            {
                if (!isAdd)
                {
                    Credentials.ScanTimeOutTrigger.AddJob();
                    EventLog.ScanTrigger.AddJob();
                    ActivityLog.ActivityLogTrigger.AddJob();
                    //CredentialAccessTime.ScanCredentialAccessTimeTrigger.AddJob();
                    isAdd = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                isAdd = false;
            }
            return isAdd;
        }
    }
}
