
namespace MOS.Filter
{
    public class HisRationScheduleViewFilter : FilterBase
    {
        public long? TREATMENT_ID__EXACT { get; set; }
        public long? LAST_DEPARTMENT_ID { get; set; }
        public long? FROM_DATE_FROM { get; set; }
        public long? FROM_DATE_TO { get; set; }
        public long? TO_DATE_FROM { get; set; }
        public long? TO_DATE_TO { get; set; }
        public long? LAST_ASSIGN_DATE_FROM { get; set; }
        public long? LAST_ASSIGN_DATE_TO { get; set; }
        public bool? IS_PAUSE { get; set; }

        public HisRationScheduleViewFilter()
            : base()
        {
        }
    }
}
