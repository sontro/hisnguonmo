using Inventec.Common.Logging;
using Inventec.Core;
using System;

namespace SAR.API.Scheduler
{
    public class ScanReportCalendarJob : Inventec.Common.Scheduler.Job
    {
        private int repeatTime;
        public int RepeatTime
        {
            get
            {
                if (repeatTime <= 0)
                {
                    try
                    {
                        repeatTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["API.Scheduler.ScanReportCalendarJob"]);
                        return repeatTime;
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Debug(ex);
                        repeatTime = 0;
                        return 3600000;
                    }
                }
                else
                {
                    return repeatTime;
                }
            }
        }

        public override void DoJob()
        {
            try
            {
                CommonParam param = new CommonParam();
                new SAR.MANAGER.Manager.SarReportCalendarManager(param).Scan();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public override string GetName()
        {
            return this.GetType().Name;
        }

        public override int GetRepetitionIntervalTime()
        {
            return RepeatTime;
        }

        public override bool IsRepeatable()
        {
            return true;
        }
    }
}