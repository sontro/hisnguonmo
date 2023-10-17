
namespace MOS.Filter
{
    public class HisHivTreatmentViewFilter : FilterBase
    {
        public long? PATIENT_ID { get; set; }
        public long? IN_TIME_FROM { get; set; }
        public long? IN_TIME_TO { get; set; }
        public HisHivTreatmentViewFilter()
            : base()
        {
        }
    }
}
