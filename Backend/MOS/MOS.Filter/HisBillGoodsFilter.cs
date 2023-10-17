
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBillGoodsFilter : FilterBase
    {
        public long? BILL_ID { get; set; }
        public List<long> BILL_IDs { get; set; }

        public HisBillGoodsFilter()
            : base()
        {
        }
    }
}
