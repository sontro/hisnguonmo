
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAppointmentPeriodCountByDateFilter : FilterBase
    {
        public long BRANCH_ID { get; set; }
        public long APPOINTMENT_DATE { get; set; }

        public HisAppointmentPeriodCountByDateFilter()
            : base()
        {
        }
    }
}
