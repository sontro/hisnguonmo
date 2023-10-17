
namespace MOS.Filter
{
    public class HisVaccAppointmentViewFilter : FilterBase
    {
        public long? VACCINATION_EXAM_ID { get; set; }
        public long? VACCINE_TYPE_ID { get; set; }

        public HisVaccAppointmentViewFilter()
            : base()
        {
        }
    }
}
