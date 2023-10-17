using MOS.MANAGER.UserSchedulerJob;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.UserScheduler
{
    public class UserSchedulerJob
    {
        public static void Start()
        {
            try
            {
                string userSchedulerJobsConfigKeys = ConfigurationManager.AppSettings["MOS.API.UserSchedulerJob.AutoRunWhenStart"];
                if (String.IsNullOrWhiteSpace(userSchedulerJobsConfigKeys))
                    return;
                var userSchedulerJobs = userSchedulerJobsConfigKeys.Split(';');
                List<string> failedUserSchedulerJobs = new List<string>();
                foreach (var item in userSchedulerJobs)
                {
                    if (String.IsNullOrWhiteSpace(item))
                        continue;
                    Inventec.Core.CommonParam param = new Inventec.Core.CommonParam();
                    UserSchedulerJobManager mng = new UserSchedulerJobManager(param);
                    var result = mng.Run(item);
                    if (result.Data)
                        Inventec.Common.Logging.LogSystem.Info("___Xu ly chay job thanh cong: " + item);
                    else
                        failedUserSchedulerJobs.Add(item);
                }
                if (failedUserSchedulerJobs.Count > 0)
                {
                    Inventec.Common.Logging.LogSystem.Error("___Xu ly chay job that bai: " + String.Join(";", failedUserSchedulerJobs));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
