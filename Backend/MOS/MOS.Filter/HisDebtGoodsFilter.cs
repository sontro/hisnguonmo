
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisDebtGoodsFilter : FilterBase
    {
        public long? DEBT_ID { get; set; }
        public List<long> DEBT_IDs { get; set; }

        public HisDebtGoodsFilter()
            : base()
        {
        }
    }
}
