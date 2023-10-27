using ACS.EFMODEL.DataModels;
using System;

namespace ACS.SDO
{
    public class TimerSDO 
    {
        public TimerSDO()
        {

        }

        public DateTime DateNow { get; set; }
        public long UniversalTime { get; set; }
        public long LocalTime { get; set; }
        public string TimeZoneId { get; set; }

    }
}
