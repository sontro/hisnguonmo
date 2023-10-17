
namespace MOS.Filter
{
    public class HisContactPointFilter : FilterBase
    {
        public long? PATIENT_ID { get; set; }
        public long? EMPLOYEE_ID { get; set; }
        public long? GENDER_ID { get; set; }
        public string FULL_NAME_EXACT { get; set; }
        public long? DOB { get; set; }
        public long? ID__NOT_EQUAL { get; set; }

        public HisContactPointFilter()
            : base()
        {
        }
    }
}
