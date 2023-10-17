using Inventec.Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Credentials
{
    internal class ScanTimeOutTrigger
    {
        internal static void AddJob()
        {
            try
            {
                TimeSpan repeatTime = TimeSpan.FromMilliseconds(0);
                if (GetRepetitionIntervalTime(ref repeatTime))
                {
                    ISchedulerFactory schedFact = new StdSchedulerFactory();

                    IScheduler sched = schedFact.GetScheduler();
                    sched.Start();

                    IJobDetail job = JobBuilder.Create<ScanTimeOutJob>().WithIdentity("ScanTimeOutCredentialsJob", "ScanTimeOutCredentials").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithIdentity("ScanTimeOutCredentialsTrigger", "ScanTimeOutCredentials").StartNow().WithSimpleSchedule(x => x.WithInterval(repeatTime).RepeatForever()).Build();
                    sched.ScheduleJob(job, trigger);
                }
                else
                {
                    LogSystem.Info("Khong cau hinh repeatTime tien trinh => khong khoi dong tien trinh ScanTimeOut");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static bool GetRepetitionIntervalTime(ref TimeSpan repeatTime)
        {
            bool result = false;
            try
            {
                //repeatTime = TimeSpan.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["MOS.QuartzScheduler.ScanTimeOutCredentialsJob"]));
                int time = int.Parse(ConfigurationManager.AppSettings["MOS.API.Scheduler.ScanTimeOutCredentialsJob"]);
                if (time <= 0) return false;
                repeatTime = TimeSpan.FromMilliseconds(time);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return result;
        }
    }
}
