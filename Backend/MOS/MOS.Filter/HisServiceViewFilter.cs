
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceViewFilter : FilterBase
    {
        public long? SERVICE_TYPE_ID { get; set; }
        public bool? IS_LEAF { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }
        public string PETROLEUM_CODE { get; set; }
        public string PETROLEUM_NAME { get; set; }

        public string SERVICE_CODE__EXACT { get; set; }

        public HisServiceViewFilter()
            : base()
        {
        }
    }
}
