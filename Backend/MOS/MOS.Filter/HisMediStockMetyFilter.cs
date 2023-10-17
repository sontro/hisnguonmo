
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediStockMetyFilter : FilterBase
    {
        public long? MEDI_STOCK_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }

        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> MEDI_STOCK_IDs { get; set; }
        
        public HisMediStockMetyFilter()
            : base()
        {
        }
    }
}
