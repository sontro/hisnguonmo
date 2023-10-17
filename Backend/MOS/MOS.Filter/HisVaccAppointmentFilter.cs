
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisVaccAppointmentFilter : FilterBase
    {
        public long? VACCINATION_EXAM_ID { get; set; }
        public long? VACCINE_TYPE_ID { get; set; }

        public List<long> VACCINATION_EXAM_IDs { get; set; }
        public List<long> VACCINE_TYPE_IDs { get; set; }

        public HisVaccAppointmentFilter()
            : base()
        {
        }
    }
}
