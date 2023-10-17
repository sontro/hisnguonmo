using Inventec.Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.MediStock
{
    class AutoSetIsNotTakenTrigger
    {
        internal static void AddJob()
        {
            try
            {
                int hours = 0;
                if (GetRepetitionIntervalTime(ref hours))
                {
                    ISchedulerFactory schedFact = new StdSchedulerFactory();

                    IScheduler sched = schedFact.GetScheduler();
                    sched.Start();

                    IJobDetail job = JobBuilder.Create<AutoSetIsNotTakenJob>().WithIdentity("AutoSetIsNotTakenJob", "AutoSetIsNotTaken").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithDailyTimeIntervalSchedule(s => s.WithIntervalInHours(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(hours, 0))).Build();
                    sched.ScheduleJob(job, trigger);
                }
                else
                {
                    LogSystem.Info("Khong cau hinh repeatTime tien trinh => khong khoi dong tien trinh AutoSetIsNotTakenJob");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static bool GetRepetitionIntervalTime(ref int repeatHours)
        {
            try
            {
                int time = int.Parse(ConfigurationManager.AppSettings["MOS.API.Scheduler.AutoSetIsNotTaken"]);
                if (time < 0) return false;
                repeatHours = time;
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
