using Inventec.Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Browser
{
    class BrowserTrigger
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

                    IJobDetail job = JobBuilder.Create<BrowserJob>().WithIdentity("BrowserJob", "Browser").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithIdentity("BrowserTrigger", "Browser").StartNow().WithSimpleSchedule(x => x.WithInterval(repeatTime).RepeatForever()).Build();
                    sched.ScheduleJob(job, trigger);
                }
                else
                {
                    LogSystem.Info("Khong cau hinh repeatTime tien trinh => khong khoi dong tien trinh Browser");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        //20 phut 1 lan goi
        public static bool GetRepetitionIntervalTime(ref TimeSpan repeatTime)
        {
            int time = 1190000;
            try
            {
                time = int.Parse(ConfigurationManager.AppSettings["MOS.API.Scheduler.BrowserJob"]);
                if (time <= 0)
                    time = 1190000;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            repeatTime = TimeSpan.FromMilliseconds(time);
            return true;
        }
    }
}
