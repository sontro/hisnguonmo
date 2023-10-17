
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisDebateFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public short? CONTENT_TYPE { get; set; }
        public long? TRACKING_ID { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }

        public HisDebateFilter()
            : base()
        {
        }
    }
}
