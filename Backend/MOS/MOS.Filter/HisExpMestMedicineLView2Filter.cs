using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestMedicineLView2Filter
    {
        public bool? HAS_CHMS_TYPE_ID { get; set; }
        public long? MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID { get; set; }
        public long? EXP_MEST_STT_ID { get; set; }
        public List<long> TDL_MEDICINE_TYPE_IDs { get; set; }

        public HisExpMestMedicineLView2Filter()
            : base()
        {
        }
    }
}
