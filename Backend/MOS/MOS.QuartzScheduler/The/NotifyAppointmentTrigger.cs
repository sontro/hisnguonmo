using Quartz;
using Quartz.Impl;
using System;

namespace MOS.QuartzScheduler.The
{
    class NotifyAppointmentTrigger
    {
        internal static void AddJob()
        {
            try
            {
                int hour = 0;
                int minute = 0;
                if (LoadConfig(ref hour))
                {
                    ISchedulerFactory schedFact = new StdSchedulerFactory();

                    IScheduler sched = schedFact.GetScheduler();
                    sched.Start();

                    IJobDetail job = JobBuilder.Create<NotifyAppointmentJob>().WithIdentity("NotifyAppointmentJob", "NotifyAppointment").Build();

                    ITrigger trigger = TriggerBuilder.Create()
                      .WithIdentity("NotifyAppointmentTrigger", "NotifyAppointment").StartNow().WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(hour, minute, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday)).Build();
                    sched.ScheduleJob(job, trigger);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool LoadConfig(ref int hour)
        {
            bool result = false;
            try
            {
                string configValue = System.Configuration.ConfigurationSettings.AppSettings["MOS.API.Scheduler.TheViet.Notify.Appointment.Time"];
                if (!String.IsNullOrWhiteSpace(configValue))
                {
                    int gio = Convert.ToInt32(configValue);
                    if (gio >= 0 && gio <= 23)
                    {
                        result = true;
                        hour = gio;
                    }
                }
                if (!result)
                {
                    Inventec.Common.Logging.LogSystem.Info("Khong cau hinh TheViet.Notify.Appointment.Time tien trinh => khong khoi dong tien trinh NotifyAppointment");
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
