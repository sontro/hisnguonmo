
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisEmployeeScheduleFilter : FilterBase
    {
        public string LOGINNAME__EXACT { get; set; }
        public List<int> SCHEDULE_DATES { get; set; }
        public int? SCHEDULE_DATE { get; set; }

        public HisEmployeeScheduleFilter()
            : base()
        {
        }
    }
}
