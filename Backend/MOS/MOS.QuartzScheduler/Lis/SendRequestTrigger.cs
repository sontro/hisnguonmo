using Inventec.Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Lis
{
    class SendRequestTrigger
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

                    IJobDetail job = JobBuilder.Create<SendRequestJob>().WithIdentity("SendRequestToLisJob", "SendRequestToLis").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithIdentity("SendRequestToLisTrigger", "SendRequestToLis").StartNow().WithSimpleSchedule(x => x.WithInterval(repeatTime).RepeatForever()).Build();
                    sched.ScheduleJob(job, trigger);
                }
                else
                {
                    LogSystem.Info("Khong cau hinh repeatTime tien trinh => khong khoi dong tien trinh SendRequestToLis");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static bool GetRepetitionIntervalTime(ref TimeSpan repeatTime)
        {
            try
            {
                int time = int.Parse(ConfigurationManager.AppSettings["MOS.API.Scheduler.SendToLisJob"]);
                if (time <= 0) return false;
                repeatTime = TimeSpan.FromMilliseconds(time);
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
    }
}
