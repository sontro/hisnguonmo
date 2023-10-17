
namespace MOS.Filter
{
    public class HisPatientObservationFilter : FilterBase
    {
        public long? TREATMENT_BED_ROOM_ID { get; set; }
        public long? OBSERVED_TIME_FROM { get; set; }
        public long? OBSERVED_TIME_TO { get; set; }

        public HisPatientObservationFilter()
            : base()
        {
        }
    }
}
