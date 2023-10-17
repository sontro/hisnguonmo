
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBillFundViewFilter : FilterBase
    {
        public List<long> BILL_IDs { get; set; }
        public long? BILL_ID { get; set; }

        public long? TREATMENT_ID { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public long? FUND_ID { get; set; }
        public List<long> FUND_IDs { get; set; }

        public HisBillFundViewFilter()
            : base()
        {
        }
    }
}
