using Inventec.Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.The
{
    class DownloadImageFromCosTrigger
    {
        internal static void AddJob()
        {
            try
            {
                int hour = 0;
                int minute = 0;
                if (LoadConfig(ref hour, ref minute))
                {
                    ISchedulerFactory schedFact = new StdSchedulerFactory();

                    IScheduler sched = schedFact.GetScheduler();
                    sched.Start();

                    IJobDetail job = JobBuilder.Create<DownloadImageFromCosJob>().WithIdentity("DownloadImageFromCosJob", "DownloadImageFromCos").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithIdentity("DownloadImageFromCosTrigger", "DownloadImageFromCos").StartNow().WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(hour, minute, DayOfWeek.Friday, DayOfWeek.Monday, DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Thursday, DayOfWeek.Tuesday, DayOfWeek.Wednesday)).Build();
                    sched.ScheduleJob(job, trigger);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool LoadConfig(ref int hour, ref int minute)
        {
            bool result = false;
            try
            {
                string configValue = System.Configuration.ConfigurationSettings.AppSettings["MOS.API.Scheduler.DownloadImageFromCosJob"];
                if (!String.IsNullOrWhiteSpace(configValue))
                {
                    string[] arrStr = configValue.Split(':');
                    if (arrStr != null && arrStr.Length == 2)
                    {
                        int gio = Convert.ToInt32(arrStr[0]);
                        int phut = Convert.ToInt32(arrStr[1]);
                        if (gio >= 0 && gio <= 23 && phut >= 0 && phut <= 59)
                        {
                            result = true;
                            hour = gio;
                            minute = phut;
                        }
                    }
                }
                if (!result)
                {
                    LogSystem.Info("Khong cau hinh repeatTime tien trinh => khong khoi dong tien trinh DownloadImageFromCos");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

    }
}
