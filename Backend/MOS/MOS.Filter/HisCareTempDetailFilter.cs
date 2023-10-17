
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCareTempDetailFilter : FilterBase
    {
        public long? CARE_TEMP_ID { get; set; }
        public long? CARE_TYPE_ID { get; set; }

        public List<long> CARE_TEMP_IDs { get; set; }
        public List<long> CARE_TYPE_IDs { get; set; }

        public HisCareTempDetailFilter()
            : base()
        {
        }
    }
}
