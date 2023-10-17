using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMedicineBeanLViewFilter
    {
        protected static readonly long NEGATIVE_ID = -1;

        public long? ID { get; set; }
        public List<long> IDs { get; set; }
        public short? IS_ACTIVE { get; set; }

        public long? MEDI_STOCK_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }
        public short? MEDICINE_TYPE_IS_ACTIVE { get; set; }
        public short? MEDICINE_IS_ACTIVE { get; set; }

        public List<long> MEDI_STOCK_IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }

        public bool? IS_BUSINESS { get; set; }

        public HisMedicineBeanLViewFilter()
        {
        }
    }
}
