using Inventec.Common.Logging;
using MOS.MANAGER.Config;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.PubSub
{
    class PubSubTrigger
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

                    IJobDetail job = JobBuilder.Create<PubSubJob>().WithIdentity("PubSubJob", "PubSub").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithIdentity("PubSubJobTrigger", "PubSubJob").StartNow().WithSimpleSchedule(x => x.WithInterval(repeatTime).RepeatForever()).Build();
                    sched.ScheduleJob(job, trigger);
                }
                else
                {
                    LogSystem.Info("Khong cau hinh repeatTime tien trinh => khong khoi dong tien trinh PubSub");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool GetRepetitionIntervalTime(ref TimeSpan repeatTime)
        {
            try
            {
                int time = (int)PubSubServerCFG.Time_Check_Connection * 1000;
                if (time < 0) return false;
                if (time == 0)
                {
                    time = 3600000;
                }

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
