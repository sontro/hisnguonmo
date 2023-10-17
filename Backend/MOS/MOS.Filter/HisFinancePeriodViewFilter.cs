
namespace MOS.Filter
{
    public class HisFinancePeriodViewFilter : FilterBase
    {
        public long? PREVIOUS_PERIOD_TIME__NULL_OR_LESS { get; set; }
        public long? BRANCH_ID { get; set; }
        public long? PREVIOUS_ID { get; set; }
        public long? PERIOD_TIME_FROM { get; set; }
        public long? PERIOD_TIME_TO { get; set; }

        public HisFinancePeriodViewFilter()
            : base()
        {
        }
    }
}
