
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisExpBltyServiceViewFilter : FilterBase
    {

        public long? EXP_MEST_ID { get; set; }
        public long? BLOOD_TYPE_ID { get; set; }
        public long? SERVICE_ID { get; set; }

        public List<long> EXP_MEST_IDs { get; set; }
        public List<long> BLOOD_TYPE_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }

        public HisExpBltyServiceViewFilter()
            : base()
        {
        }
    }
}
