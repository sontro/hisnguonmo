
namespace MOS.Filter
{
    public class HisMestPeriodBloodViewFilter : FilterBase
    {
        public long? MEDI_STOCK_PERIOD_ID { get; set; }
        public long? BLOOD_TYPE_ID { get; set; }

        public HisMestPeriodBloodViewFilter()
            : base()
        {
        }
    }
}
