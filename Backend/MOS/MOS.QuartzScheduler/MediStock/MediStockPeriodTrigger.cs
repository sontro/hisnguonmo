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
    class MediStockPeriodTrigger
    {
        /// <summary>
        /// <Seconds> <Minutes> <Hours> <Day-of-Month> <Month> <Day-of-Week> <Year (optional field)>
        /// Seconds: 0 - 59
        /// Minutes: 0 - 59
        /// Hours: 0 - 23
        /// Day-of-Month : 0 - 31
        /// Month: 0 - 11 | JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC
        /// Day-of-Week: 1 - 7 (1 = Sunday) | SUN, MON, TUE, WED, THU, FRI, SAT
        /// The ‘/’ character can be used to specify increments to values.
        /// For example, if you put ‘0/15’ in the Minutes field, it means ‘every 15 minutes, starting at minute zero’.
        /// If you used ‘3/20’ in the Minutes field, it would mean ‘every 20 minutes during the hour, starting at minute three’ - or in other words it is the same as specifying ‘3,23,43’ in the Minutes field
        /// </summary>
        internal static void AddJob()
        {
            try
            {
                //TimeSpan repeatTime = TimeSpan.FromMilliseconds(0);
                //if (GetRepetitionIntervalTime(ref repeatTime))
                {
                    ISchedulerFactory schedFact = new StdSchedulerFactory();

                    IScheduler sched = schedFact.GetScheduler();
                    sched.Start();

                    IJobDetail job = JobBuilder.Create<MediStockPeriodJob>().WithIdentity("MediStockPeriodJob", "MediStockPeriod").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithIdentity("MediStockPeriodTrigger", "MediStockPeriod").StartNow().WithCronSchedule("0 5 0 1 * ?").Build();
                    //.WithIdentity("MediStockPeriodTrigger", "MediStockPeriod").StartNow().WithSimpleSchedule(x => x.WithInterval(repeatTime).RepeatForever()).Build();
                    sched.ScheduleJob(job, trigger);
                }
                //else
                //{
                //    LogSystem.Info("Khong cau hinh repeatTime tien trinh => khong khoi dong tien trinh MediStockPeriod");
                //}
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
                //repeatTime = TimeSpan.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["MOS.QuartzScheduler.RefreshMediStockStatusJob"]));
                int time = int.Parse(ConfigurationManager.AppSettings["MOS.API.Scheduler.MediStockPeriodJob"]);
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
