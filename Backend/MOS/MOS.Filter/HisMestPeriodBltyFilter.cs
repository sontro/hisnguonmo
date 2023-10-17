
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMestPeriodBltyFilter : FilterBase
    {
        public long? MEDI_STOCK_PERIOD_ID { get; set; }
        public long? BLOOD_TYPE_ID { get; set; }

        public List<long> MEDI_STOCK_PERIOD_IDs { get; set; }
        public List<long> BLOOD_TYPE_IDs { get; set; }
        public bool? IS_ERROR { get; set; }
        public bool? IS_EMPTY { get; set; }
        public bool? IS_NO_CHANGE { get; set; }

        public HisMestPeriodBltyFilter()
            : base()
        {
        }
    }
}
