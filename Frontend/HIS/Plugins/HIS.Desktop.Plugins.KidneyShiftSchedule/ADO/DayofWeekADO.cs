using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KidneyShiftSchedule.ADO
{
    internal class DayofWeekADO
    {
        internal DayofWeekADO()
        {
        }
        public int Day { get; set; }
        public string DayofWeek { get; set; }

        internal List<DayofWeekADO> DayofWeekADOs
        {
            get
            {
                List<DayofWeekADO> rs = new List<DayofWeekADO>();
                rs.Add(new DayofWeekADO() { Day = 1, DayofWeek = "2" });
                rs.Add(new DayofWeekADO() { Day = 2, DayofWeek = "3" });
                rs.Add(new DayofWeekADO() { Day = 3, DayofWeek = "4" });
                rs.Add(new DayofWeekADO() { Day = 4, DayofWeek = "5" });
                rs.Add(new DayofWeekADO() { Day = 5, DayofWeek = "6" });
                rs.Add(new DayofWeekADO() { Day = 6, DayofWeek = "7" });
                rs.Add(new DayofWeekADO() { Day = 0, DayofWeek = "CN" });
                return rs;
            }
        }

    }
}
