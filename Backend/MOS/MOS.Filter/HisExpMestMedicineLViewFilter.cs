using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestMedicineLViewFilter
    {
        public long? ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? REPLACE_MEDICINE_TYPE_ID { get; set; }
        public long? TDL_MEDI_STOCK_ID { get; set; }
        public long? EXP_MEST_TYPE_ID { get; set; }

        public List<long> IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> REPLACE_MEDICINE_TYPE_IDs { get; set; }
        public List<long> TDL_MEDI_STOCK_IDs { get; set; }
        public List<long> EXP_MEST_TYPE_IDs { get; set; }

        public bool? IS_REPLACE { get; set; }

        public HisExpMestMedicineLViewFilter()
            : base()
        {
        }
    }
}
