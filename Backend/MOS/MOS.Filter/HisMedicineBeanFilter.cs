using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMedicineBeanFilter : FilterBase
    {
        public List<long> MEDICINE_IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<string> DETACH_KEYs { get; set; }
        public List<long> EXP_MEST_MEDICINE_IDs { get; set; }
        public List<long> NOT_IN_IDs { get; set; }

        public long? MEDICINE_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public long? EXP_MEST_MEDICINE_ID { get; set; }
        
        public string DETACH_KEY__EXACT { get; set; }
        public short? MEDICINE_IS_ACTIVE { get; set; }
        public long? EXPIRED_DATE_NULl__OR__GREATER_THAN__OR__EQUAL { get; set; }

        public List<long> ACTIVE__OR__EXP_MEST_MEDICINE_IDs { get; set; }

        public bool? HAS_EXP_MEST_MEDICINE_ID { get; set; }

        public HisMedicineBeanFilter()
            : base()
        {
        }
    }
}
