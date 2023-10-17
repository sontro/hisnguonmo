using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSereServView14Filter : FilterBase
    {
        public List<long> TREATMENT_IDs { get; set; }

        public bool? IS_EXPEND { get; set; }
        public bool? HAS_MUST_PAY_PRICE { get; set; }
        public bool? HAS_EXECUTE { get; set; }

        public long? TREATMENT_ID { get; set; }

        public HisSereServView14Filter()
            : base()
        {

        }
    }
}
