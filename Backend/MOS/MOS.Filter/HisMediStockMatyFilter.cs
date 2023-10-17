
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediStockMatyFilter : FilterBase
    {
        public long? MATERIAL_TYPE_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> MEDI_STOCK_IDs { get; set; }

        public HisMediStockMatyFilter()
            : base()
        {
        }
    }
}
