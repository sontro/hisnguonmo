using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMaterialBeanLViewFilter
    {
        protected static readonly long NEGATIVE_ID = -1;

        public long? ID { get; set; }
        public List<long> IDs { get; set; }
        public short? IS_ACTIVE { get; set; }

        public long? MEDI_STOCK_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }
        public short? MATERIAL_TYPE_IS_ACTIVE { get; set; }
        public short? MATERIAL_IS_ACTIVE { get; set; }

        public List<long> MEDI_STOCK_IDs { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }

        public bool? IS_BUSINESS { get; set; }

        public HisMaterialBeanLViewFilter()
        {
        }
    }
}
