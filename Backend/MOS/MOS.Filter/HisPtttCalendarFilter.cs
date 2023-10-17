
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPtttCalendarFilter : FilterBase
    {
        public List<long> DEPARTMENT_IDs { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? TIME_FROM_FROM { get; set; }
        public long? TIME_FROM_TO { get; set; }
        public long? TIME_TO_FROM { get; set; }
        public long? TIME_TO_TO { get; set; }
        public long? DATE_FROM__FROM { get; set; }
        public long? DATE_TO__TO { get; set; }

        public HisPtttCalendarFilter()
            : base()
        {
        }
    }
}
