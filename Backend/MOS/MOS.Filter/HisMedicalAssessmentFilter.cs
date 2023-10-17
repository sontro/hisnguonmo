
namespace MOS.Filter
{
    public class HisMedicalAssessmentFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public short? IS_DELETE { get; set; }

        public HisMedicalAssessmentFilter()
            : base()
        {
        }
    }
}
