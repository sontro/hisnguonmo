
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisDispenseViewFilter : FilterBase
    {
        public List<long> MEDI_STOCK_IDs { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public long? DISPENSE_TYPE_ID { get; set; }
        public List<long> DISPENSE_TYPE_IDs { get; set; }
        public long? DISPENSE_DATE_FROM { get; set; }
        public long? DISPENSE_DATE_TO { get; set; }
        
        public string DISPENSE_CODE__EXACT { get; set; }

        public HisDispenseViewFilter()
            : base()
        {
        }
    }
}
