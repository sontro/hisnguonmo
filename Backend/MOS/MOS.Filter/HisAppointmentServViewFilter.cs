
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAppointmentServViewFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? SERVICE_ID { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }

        public List<long> TREATMENT_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }

        public HisAppointmentServViewFilter()
            : base()
        {
        }
    }
}
