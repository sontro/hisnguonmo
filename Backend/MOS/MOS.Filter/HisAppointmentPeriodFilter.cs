
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAppointmentPeriodFilter : FilterBase
    {
        public long? BRANCH_ID { get; set; }
        public List<long> BRANCH_IDs { get; set; }

        public HisAppointmentPeriodFilter()
            : base()
        {
        }
    }
}
