
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisImpMestPayViewFilter : FilterBase
    {
        public string PAYER_LOGINNAME__EXACT { get; set; }
        public string SUPPLIER_CODE__EXACT { get; set; }

        public long? IMP_MEST_PROPOSE_ID { get; set; }
        public long? PAY_FORM_ID { get; set; }
        public long? SUPPLIER_ID { get; set; }

        public List<long> IMP_MEST_PROPOSE_IDs { get; set; }
        public List<long> PAY_FORM_IDs { get; set; }
        public List<long> SUPPLIER_IDs { get; set; }

        public long? PAY_TIME_FROM { get; set; }
        public long? PAY_TIME_TO { get; set; }

        public long? NEXT_PAY_TIME_FROM { get; set; }
        public long? NEXT_PAY_TIME_TO { get; set; }

        public HisImpMestPayViewFilter()
            : base()
        {
        }
    }
}
