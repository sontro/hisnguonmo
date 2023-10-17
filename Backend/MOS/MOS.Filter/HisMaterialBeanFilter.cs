using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMaterialBeanFilter : FilterBase
    {
        public long? MATERIAL_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public long? EXP_MEST_MATERIAL_ID { get; set; }
        public long? EXPIRED_DATE_NULl__OR__GREATER_THAN__OR__EQUAL { get; set; }
        public List<long> NOT_IN_IDs { get; set; }
        public List<long> MATERIAL_IDs { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> EXP_MEST_MATERIAL_IDs { get; set; }
        public List<string> DETACH_KEYs { get; set; }
        public short? MATERIAL_IS_ACTIVE { get; set; }
        public List<long> ACTIVE__OR__EXP_MEST_MATERIAL_IDs { get; set; }
        public List<string> SERIAL_NUMBERs { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public List<long> MEDI_STOCK_IDs { get; set; }

        public bool? HAS_EXP_MEST_MATERIAL_ID { get; set; }
        public bool? HAS_MEDI_STOCK_ID { get; set; }

        public HisMaterialBeanFilter()
            : base()
        {
        }
    }
}
