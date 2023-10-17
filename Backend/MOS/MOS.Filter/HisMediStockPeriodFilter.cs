
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediStockPeriodFilter : FilterBase
    {
        public bool? IS_AUTO_PERIOD { get; set; }

        public long? MEDI_STOCK_ID { get; set; }
        public long? PREVIOUS_ID { get; set; }
        public long? TO_TIME_FROM { get; set; }
        public long? TO_TIME_TO { get; set; }
        public long? TO_TIME { get; set; }

        public string MEDI_STOCK_PERIOD_NAME { get; set; }

        public List<long> MEDI_STOCK_IDs { get; set; }

        public HisMediStockPeriodFilter()
            : base()
        {
        }
    }
}
