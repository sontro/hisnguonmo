
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMestMatyDepaFilter : FilterBase
    {
        public long? MATERIAL_TYPE_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }

        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> MEDI_STOCK_IDs { get; set; }

        public bool? HAS_MEDI_STOCK_ID { get; set; }

        public HisMestMatyDepaFilter()
            : base()
        {
        }
    }
}
