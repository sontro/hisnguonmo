using System.Collections.Generic;

namespace MOS.Filter
{
    public class DHisExpMestDetail1Filter
    {
        public List<long> EXP_MEST_IDs { get; set; }

        public long? EXP_MEST_TYPE_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public long? BILL_ID { get; set; }
        public long? DEBT_ID { get; set; }
        public string TDL_TREATMENT_CODE__EXACT { get; set; }
        public string EXP_MEST_CODE__EXACT { get; set; }
        public bool? HAS_BILL { get; set; }
        public bool? HAS_DEBT { get; set; }

        public DHisExpMestDetail1Filter()
            : base()
        {
        }
    }
}
