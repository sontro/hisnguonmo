
using System.Collections.Generic;
namespace MOS.Filter
{
    public class DHisMediStock2Filter
    {
        public List<long> MEDI_STOCK_IDs { get; set; }
        public bool? IS_VACCINE { get; set; }
        public bool? IS_REUSABLE { get; set; }
        public long? EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL { get; set; }

        public DHisMediStock2Filter()
            : base()
        {
        }
    }
}
