
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAppointmentPeriodViewFilter : FilterBase
    {
        public long? BRANCH_ID { get; set; }
        public List<long> BRANCH_IDs { get; set; }

        public HisAppointmentPeriodViewFilter()
            : base()
        {
        }
    }
}
