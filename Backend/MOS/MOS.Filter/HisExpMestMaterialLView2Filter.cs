using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestMaterialLView2Filter
    {
        public bool? HAS_CHMS_TYPE_ID { get; set; }
        public long? MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID { get; set; }
        public long? EXP_MEST_STT_ID { get; set; }
        public List<long> TDL_MATERIAL_TYPE_IDs { get; set; }

        public HisExpMestMaterialLView2Filter()
            : base()
        {
        }
    }
}
