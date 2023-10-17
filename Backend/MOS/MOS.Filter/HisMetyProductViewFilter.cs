
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMetyProductViewFilter : FilterBase
    {
        public string MEDICINE_TYPE_CODE__EXACT { get; set; }
        public string SERVICE_UNIT_CODE__EXACT { get; set; }
        public string MEDICINE_LINE_CODE__EXACT { get; set; }

        public long? MEDICINE_TYPE_ID { get; set; }
        public long? TDL_SERVICE_UNIT_ID { get; set; }
        public long? MEDICINE_LINE_ID { get; set; }

        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> TDL_SERVICE_UNIT_IDs { get; set; }
        public List<long> MEDICINE_LINE_IDs { get; set; }

        public HisMetyProductViewFilter()
            : base()
        {
        }
    }
}
